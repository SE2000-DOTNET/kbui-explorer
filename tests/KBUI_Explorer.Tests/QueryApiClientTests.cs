using KBUI_Explorer.Core;
using Xunit;

namespace KBUI_Explorer.Tests;

public class QueryValidatorTests
{
    [Fact]
    public void ValidateQuestion_ReturnsError_WhenQuestionIsEmpty()
    {
        var result = QueryValidator.ValidateQuestion("   ", 500);

        Assert.Equal("question is required.", result);
    }

    [Fact]
    public void ValidateQuestion_ReturnsError_WhenQuestionExceedsMaxLength()
    {
        var longQuestion = new string('x', 501);

        var result = QueryValidator.ValidateQuestion(longQuestion, 500);

        Assert.Equal("question exceeds max length (500 characters).", result);
    }

    [Fact]
    public void ClampTopK_UsesConfiguredUpperBound()
    {
        Assert.Equal(10, QueryValidator.ClampTopK(999, 10));
        Assert.Equal(1, QueryValidator.ClampTopK(0, 10));
        Assert.Equal(4, QueryValidator.ClampTopK(4, 10));
    }

    [Fact]
    public void IsAllowedBaseUrl_ReturnsFalse_WhenUrlContainsPath()
    {
        var result = QueryValidator.IsAllowedBaseUrl("https://example.com/api");

        Assert.False(result);
    }
}
