using System;
using System.Linq;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class PlaceholderParserTests
{
    [Theory]
    [InlineData("($test$)", "(value )", "value ")]
    [InlineData("($test$ )", "(value )", "value")]
    [InlineData("($test$)", "(value (test))", "value (test)")]
    [InlineData("($test$)", "(value (test) )", "value (test) ")]
    [InlineData("($test$):", "(value (test) );", "value (test) ")]
    [InlineData("($test$ )", "(value (test) )", "value (test)")]
    [InlineData("$test$(123)", "temp1(123)", "temp1")]
    
    [InlineData("$var$;", "test;",  "test")]
    [InlineData("$var$;", "test;;",  "test")]
    [InlineData("$var$;.", "test;;;.", "test;;")]
    [InlineData("$var$", "temp1 ?? temp2", "temp1 ?? temp2")]
    [InlineData("$var$;", "temp1 ?? temp2;", "temp1 ?? temp2")]
    [InlineData("$var$;.", "temp1 ?? temp2;.", "temp1 ?? temp2")]
    public static void PlaceholderParsingShouldBeSuccess(string template, string source, string expectedResult)
    {
        var input = Input.Input.String(source);
        var templateParser = StructuralSearch.StructuralSearch.ParseFindTemplate(template);
        var matches = templateParser.Parse(input);
        Assert.Single(matches);
        var match = matches.First();
        var placeholder = match.Placeholders.First();
        Assert.Equal(expectedResult, placeholder.Value.Value);
    }
}