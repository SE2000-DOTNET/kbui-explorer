using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using KBUI_Explorer.Models;
using KBUI_Explorer.Options;
using Microsoft.Extensions.Options;

namespace KBUI_Explorer.Services;

/// <summary>
/// Restricted HTTP client for Rag_Ingestion_Tool query endpoints only.
/// Hard-blocks ingest, index delete, and job APIs — this app cannot call them.
/// </summary>
public sealed class QueryApiClient
{
    private static readonly HashSet<string> AllowedPaths = new(StringComparer.OrdinalIgnoreCase)
    {
        "/health",
        "/search",
        "/rag/query",
        "/chat"
    };

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        // Chat omits TopK; serializing "topK":null makes the Azure API return HTTP 400
        // because its SearchRequest.TopK is a non-nullable int.
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly QueryUiOptions _options;

    public QueryApiClient(IHttpClientFactory httpClientFactory, IOptions<QueryUiOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public string ApiBaseUrl => (_options.ApiBaseUrl ?? "").TrimEnd('/');
    public int MaxQuestionLength => Math.Max(1, _options.MaxQuestionLength);
    public int MaxTopK => Math.Max(1, _options.MaxTopK);

    public void UpdateConnection(string apiBaseUrl, string? apiKey)
    {
        var url = (apiBaseUrl ?? "").Trim().TrimEnd('/');
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new InvalidOperationException("API base URL must be an absolute http or https URL.");
        }

        // Refuse obvious admin hosts being used as "base" with path tricks — base must be origin only.
        if (!string.IsNullOrEmpty(uri.AbsolutePath) && uri.AbsolutePath != "/")
        {
            throw new InvalidOperationException("API base URL must not include a path (use origin only, e.g. https://app-q-cpy-hud-dev.azurewebsites.net).");
        }

        _options.ApiBaseUrl = url;
        _options.ApiKey = (apiKey ?? "").Trim();
        Preferences.Default.Set("QueryUi.ApiBaseUrl", _options.ApiBaseUrl);
        Preferences.Default.Set("QueryUi.ApiKey", _options.ApiKey);
    }

    public string? ValidateQuestion(string? question)
    {
        if (string.IsNullOrWhiteSpace(question))
            return "question is required.";
        if (question.Length > MaxQuestionLength)
            return $"question exceeds max length ({MaxQuestionLength} characters).";
        return null;
    }

    public int ClampTopK(int topK) => Math.Clamp(topK, 1, MaxTopK);

    public async Task<HealthResponse> GetHealthAsync(CancellationToken ct = default)
    {
        using var response = await SendAsync(HttpMethod.Get, "/health", body: null, ct);
        return await ReadAsync<HealthResponse>(response, ct);
    }

    public async Task<SearchResponse> SearchAsync(string question, int topK, CancellationToken ct = default)
    {
        var err = ValidateQuestion(question);
        if (err is not null) throw new InvalidOperationException(err);

        var body = new SearchRequest { Question = question.Trim(), TopK = ClampTopK(topK) };
        using var response = await SendAsync(HttpMethod.Post, "/search", body, ct);
        return await ReadAsync<SearchResponse>(response, ct);
    }

    public async Task<RagResponse> RagAsync(string question, int topK, CancellationToken ct = default)
    {
        var err = ValidateQuestion(question);
        if (err is not null) throw new InvalidOperationException(err);

        var body = new SearchRequest { Question = question.Trim(), TopK = ClampTopK(topK) };
        using var response = await SendAsync(HttpMethod.Post, "/rag/query", body, ct);
        return await ReadAsync<RagResponse>(response, ct);
    }

    public async Task<ChatResponse> ChatAsync(string question, CancellationToken ct = default)
    {
        var err = ValidateQuestion(question);
        if (err is not null) throw new InvalidOperationException(err);

        var body = new SearchRequest { Question = question.Trim() };
        using var response = await SendAsync(HttpMethod.Post, "/chat", body, ct);
        return await ReadAsync<ChatResponse>(response, ct);
    }

    private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string path, object? body, CancellationToken ct)
    {
        if (!AllowedPaths.Contains(path))
            throw new InvalidOperationException($"Blocked: '{path}' is not a query endpoint. KBUI Explorer is query-only.");

        // Defense in depth: never allow path traversal / absolute URLs in path.
        if (path.Contains("..", StringComparison.Ordinal) || path.Contains("://", StringComparison.Ordinal) || !path.StartsWith('/'))
            throw new InvalidOperationException("Blocked: invalid path.");

        var client = _httpClientFactory.CreateClient("KbQuery");
        client.BaseAddress = new Uri(ApiBaseUrl.TrimEnd('/') + "/");
        client.DefaultRequestHeaders.Remove("X-Api-Key");
        if (!string.IsNullOrWhiteSpace(_options.ApiKey))
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-Api-Key", _options.ApiKey);

        using var request = new HttpRequestMessage(method, path.TrimStart('/'));
        if (body is not null)
            request.Content = JsonContent.Create(body, options: JsonOptions);

        return await client.SendAsync(request, ct);
    }

    private static async Task<T> ReadAsync<T>(HttpResponseMessage response, CancellationToken ct)
    {
        var raw = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            string message;
            try
            {
                var err = JsonSerializer.Deserialize<ErrorResponse>(raw, JsonOptions);
                message = err?.Error ?? $"HTTP {(int)response.StatusCode}";
            }
            catch
            {
                message = $"HTTP {(int)response.StatusCode}";
            }
            throw new InvalidOperationException(message);
        }

        var data = JsonSerializer.Deserialize<T>(raw, JsonOptions);
        if (data is null)
            throw new InvalidOperationException("Empty or invalid JSON response.");
        return data;
    }
}
