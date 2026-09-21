namespace KBUI_Explorer.Core;

public static class QueryValidator
{
    public static string? ValidateQuestion(string? question, int maxQuestionLength)
    {
        if (string.IsNullOrWhiteSpace(question))
            return "question is required.";

        if (question.Length > maxQuestionLength)
            return $"question exceeds max length ({maxQuestionLength} characters).";

        return null;
    }

    public static int ClampTopK(int topK, int maxTopK)
    {
        return Math.Clamp(topK, 1, maxTopK);
    }

    public static bool IsAllowedBaseUrl(string? apiBaseUrl)
    {
        var url = (apiBaseUrl ?? "").Trim();
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return false;

        if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            return false;

        return uri.AbsolutePath == "/";
    }
}
