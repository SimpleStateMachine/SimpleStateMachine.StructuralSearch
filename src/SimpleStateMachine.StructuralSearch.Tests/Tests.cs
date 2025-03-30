using Pidgin;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Extensions;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class Tests
{
    [Theory]
    [InlineData("temp1 ?? temp2;", ";", "temp1 ?? temp2")]
    [InlineData("(value )", null, "value ")]
    [InlineData("(value )", null, "value")]
    [InlineData("(value (test))", null, "value (test)")]
    [InlineData("(value (test) )", null, "value (test) ")]
    [InlineData("test;;",  ";", "test;")]
    [InlineData("test;;;.", ".", "test;;")]
    [InlineData( "temp1(123)", null,"temp1")]
    [InlineData("temp1 ?? temp2", null, "temp1 ?? temp2")]
    public static void PlaceholderParsingShouldBeSuccess(string input, string? terminator, string expectedResult)
    {
        var terminatorParser = terminator is null ? Parser.EndOfLine : Parser.String(terminator);
        var result = Parser.AnyCharExcept().AtLeastOnceAsStringUntil(terminatorParser).ParseOrThrow(expectedResult);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("temp1;", ";", "temp1 ?? temp2")]
    public static void PlaceholderParsingShouldBeSuccess2(string input, string terminator, string expectedResult)
    {
        var terminatorParser = Parser.String(terminator).Try().Select(x => Unit.Value);
        var result = PlaceholderParser.CreateParser(terminatorParser).ParseToEnd(input);
        Assert.Equal(expectedResult, result);
    }
}