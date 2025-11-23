using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class Tests
{
    [Theory]
    [InlineData("test;",  ";", "test")]
    [InlineData("test;;",  ";", "test")]
    [InlineData("test;;.", ".", "test;;")]
    public static void PlaceholderParsingShouldBeSuccess2(string input, string terminator, string expectedResult)
    {
        var terminatorParser = Parser.String(terminator).Lookahead().WithDebug("terminator").Select(x => Unit.Value);
        var result = PlaceholderParser.CreateParser(terminatorParser).ParseOrThrow(input);
        Assert.Equal(expectedResult, result);
    }
}