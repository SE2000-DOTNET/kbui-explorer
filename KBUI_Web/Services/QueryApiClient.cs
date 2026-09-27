using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using KBUI_Explorer.Core;
using KBUI_Explorer.Models;
using KBUI_Explorer.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.Extensions.Options;

namespace KBUI_Explorer.Services;

/// <summary>
/// Restricted HTTP client for Rag_Ingestion_Tool query endpoints only.
/// Hard-blocks ingest, index delete, and job APIs — this app cannot call them.
/// </summary>
public sealed class QueryApiClient : IDisposable
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
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly QueryUiOptions _options;
    private readonly NavigationManager _nav;
    private HttpClient? _client;
    private string _clientBase = "";

    public QueryApiClient(IOptions<QueryUiOptions> options, NavigationManager nav)
    {
        _options = options.Value;
        _nav = nav;
    }

    public string ApiBaseUrl => (_options.ApiBaseUrl ?? "").TrimEnd('/');
    public string ApiKey => _options.ApiKey ?? "";
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

        if (!string.IsNullOrEmpty(uri.AbsolutePath) && uri.AbsolutePath != "/")
        {
            throw new InvalidOperationException("API base URL must not include a path (use origin only, e.g. https://app-q-cpy-hud-dev.azurewebsites.net).");
        }

        _options.ApiBaseUrl = url;
        _options.ApiKey = (apiKey ?? "").Trim();
        ResetClient();
    }

    public string? ValidateQuestion(string? question)
    {
        return QueryValidator.ValidateQuestion(question, MaxQuestionLength);
    }

    public int ClampTopK(int topK) => QueryValidator.ClampTopK(topK, MaxTopK);

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
        var rag = await ReadAsync<RagResponse>(response, ct);
        EnsureSpecialEndCitation(rag);
        return rag;
    }

    public async Task<ChatResponse> ChatAsync(string question, CancellationToken ct = default)
    {
        var err = ValidateQuestion(question);
        if (err is not null) throw new InvalidOperationException(err);

        var body = new SearchRequest { Question = question.Trim() };
        using var response = await SendAsync(HttpMethod.Post, "/chat", body, ct);
        return await ReadAsync<ChatResponse>(response, ct);
    }

    public void Dispose()
    {
        ResetClient();
    }

    private HttpClient GetClient()
    {
        var baseUrl = ApiBaseUrl.TrimEnd('/') + "/";
        if (_client is null || !string.Equals(_clientBase, baseUrl, StringComparison.Ordinal))
        {
            ResetClient();
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _clientBase = baseUrl;
        }

        return _client;
    }

    private void ResetClient()
    {
        _client?.Dispose();
        _client = null;
        _clientBase = "";
    }

    private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string path, object? body, CancellationToken ct)
    {
        if (!AllowedPaths.Contains(path))
            throw new InvalidOperationException($"Blocked: '{path}' is not a query endpoint. KBUI Explorer is query-only.");

        if (path.Contains("..", StringComparison.Ordinal) || path.Contains("://", StringComparison.Ordinal) || !path.StartsWith('/'))
            throw new InvalidOperationException("Blocked: invalid path.");

        var client = GetClient();
        using var request = new HttpRequestMessage(method, path.TrimStart('/'));
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Omit);
        request.SetBrowserRequestMode(BrowserRequestMode.Cors);

        if (!string.IsNullOrWhiteSpace(_options.ApiKey))
            request.Headers.TryAddWithoutValidation("X-Api-Key", _options.ApiKey);

        if (body is not null)
            request.Content = JsonContent.Create(body, options: JsonOptions);

        try
        {
            return await client.SendAsync(request, ct);
        }
        catch (Exception ex) when (IsBrowserFetchFailure(ex))
        {
            var origin = new Uri(_nav.BaseUri).GetLeftPart(UriPartial.Authority);
            throw new InvalidOperationException(
                $"Could not reach {ApiBaseUrl}{path} from this page. " +
                $"Azure CORS must allow this exact origin: {origin} (no path or trailing slash). " +
                "In the App Service go to API → CORS and add that origin plus https://se2000-dotnet.github.io. " +
                "Allow methods GET, POST, OPTIONS and header Content-Type (and X-Api-Key if the API requires a key).",
                ex);
        }
    }

    private static bool IsBrowserFetchFailure(Exception ex)
    {
        for (var current = ex; current is not null; current = current.InnerException)
        {
            if (current.Message.Contains("Failed to fetch", StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private static void EnsureSpecialEndCitation(RagResponse rag)
    {
        if (rag.Citations.Any(c => c.IsEndCitation))
            return;

        rag.Citations.Add(new Citation
        {
            Title = "OS guidance",
            SourceFile = "system://os",
            Kind = "os",
            IsSpecial = true
        });
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
