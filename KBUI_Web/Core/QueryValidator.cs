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

        if (uri.Scheme == Uri.UriSchemeHttp && !IsLoopbackHost(uri.Host))
            return false;

        return uri.AbsolutePath == "/" || string.IsNullOrEmpty(uri.AbsolutePath);
    }

    public static bool IsLoopbackHost(string? host)
    {
        if (string.IsNullOrWhiteSpace(host))
            return false;

        return host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
            || host.Equals("127.0.0.1", StringComparison.OrdinalIgnoreCase)
            || host.Equals("[::1]", StringComparison.OrdinalIgnoreCase)
            || host.Equals("::1", StringComparison.OrdinalIgnoreCase);
    }
}
