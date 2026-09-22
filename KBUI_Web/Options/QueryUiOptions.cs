namespace KBUI_Explorer.Options;

/// <summary>Connection settings for the Rag_Ingestion_Tool query API only.</summary>
public sealed class QueryUiOptions
{
    public const string SectionName = "QueryUi";

    /// <summary>Base URL of the query API (no trailing slash).</summary>
    public string ApiBaseUrl { get; set; } = "https://app-q-cpy-hud-dev.azurewebsites.net";

    /// <summary>Optional X-Api-Key when the API has demo hardening enabled.</summary>
    public string ApiKey { get; set; } = "";

    /// <summary>Client-side max question length (matches API demo default).</summary>
    public int MaxQuestionLength { get; set; } = 500;

    /// <summary>Client-side max topK (matches API demo default).</summary>
    public int MaxTopK { get; set; } = 10;
}
