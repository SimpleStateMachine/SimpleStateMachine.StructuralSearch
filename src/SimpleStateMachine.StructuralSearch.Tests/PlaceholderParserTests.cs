using System.Linq;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class PlaceholderParserTests
{
    [Theory]
    [InlineData("($test$)", "(value )", "value ")]
    [InlineData("($test$ )", "(value )", "value")]
    [InlineData("($test$)", "(value (test))", "value (test)")]
    [InlineData("($test$)", "(value (test) )", "value (test) ")]
    public static void TemplateParsingShouldBeSuccess(string template, string source, string result)
    {
        var input = Input.String(source);
        IParsingContext parsingContext = new ParsingContext(input);
        var templateParser = StructuralSearch.ParseFindTemplate(template);
        var matches = templateParser.Parse(ref parsingContext);

        Assert.Single(matches);
        var match = matches.First();
        Assert.Single(match.Placeholders);
        var placeholder = match.Placeholders.First();
            
        Assert.Equal(placeholder.Value.Value, result);
    }
        
    [Theory]
    [InlineData("$var$;", "test;;",  "test")]
    [InlineData("$var$;.", "test;;;.", "test;;")]
    public static void TemplateParsingShouldBeSuccess2(string template, string source, params string[] values)
    {
        var input = Input.String(source);
        IParsingContext parsingContext = new ParsingContext(input);
        var templateParser = StructuralSearch.ParseFindTemplate(template);
        var matches = templateParser.Parse(ref parsingContext);
        Assert.Single(matches);
        var match = matches.First();
        Assert.Equal(match.Placeholders.Count, values.Length);
        Assert.Equal(match.Placeholders.Select(x=> x.Value.Value), values);
    }
}