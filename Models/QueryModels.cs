using System.Text.Json.Serialization;

namespace KBUI_Explorer.Models;

public sealed class HealthResponse
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("profile")]
    public string? Profile { get; set; }

    [JsonPropertyName("index")]
    public string? Index { get; set; }

    [JsonPropertyName("search_endpoint")]
    public string? SearchEndpoint { get; set; }

    [JsonPropertyName("rag")]
    public bool Rag { get; set; }

    [JsonPropertyName("chat")]
    public bool Chat { get; set; }
}

public sealed class SearchRequest
{
    [JsonPropertyName("question")]
    public string Question { get; set; } = "";

    [JsonPropertyName("topK")]
    public int? TopK { get; set; }
}

public sealed class ErrorResponse
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}

public sealed class SearchHit
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("sourceFile")]
    public string? SourceFile { get; set; }

    [JsonPropertyName("source_file")]
    public string? SourceFileSnake { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("score")]
    public double? Score { get; set; }

    public string DisplaySource => SourceFile ?? SourceFileSnake ?? "";
}

public sealed class Citation
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("source_file")]
    public string? SourceFile { get; set; }

    [JsonPropertyName("score")]
    public double? Score { get; set; }
}

public sealed class SearchResponse
{
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("hits")]
    public List<SearchHit> Hits { get; set; } = new();
}

public sealed class RagResponse
{
    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("citations")]
    public List<Citation> Citations { get; set; } = new();

    [JsonPropertyName("hits")]
    public List<SearchHit> Hits { get; set; } = new();
}

public sealed class ChatResponse
{
    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }
}

public enum QueryMode
{
    Search,
    Rag,
    Chat
}
