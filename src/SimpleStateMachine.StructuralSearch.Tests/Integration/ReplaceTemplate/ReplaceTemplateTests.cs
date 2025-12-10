using SimpleStateMachine.StructuralSearch.Context;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Integration.ReplaceTemplate;

public static class ReplaceTemplateTests
{
    [Theory]
    [InlineData("$var$ = $value1$ ?? $value2$;", "temp = var1 ?? var2;",
        new[] { "var", "value1", "value2" },
        new[] { "temp", "var1", "var2" })]
    [InlineData("$var$ ??= $value$;", "temp ??= 12;",
        new[] { "var", "value" },
        new[] { "temp", "12" })]
    [InlineData("return $condition$? $value1$ : $value2$;", "return temp == 125? 12 : 15;",
        new[] { "condition", "value1", "value2" },
        new[] { "temp == 125", "12", "15" })]
    public static void ReplaceBuildShouldBeSuccess(string replaceTemplate, string resultStr, string[] keys, string[] values)
    {
        var replaceBuilder = Parsing.StructuralSearch.ParseReplaceTemplate(replaceTemplate);

        IParsingContext parsingContext = new ParsingContext(Input.Input.Empty, []);
        for (var i = 0; i < keys.Length; i++)
            parsingContext[keys[i]] = Placeholder.Placeholder.CreateEmpty(keys[i], values[i]);

        var result = replaceBuilder.Build(ref parsingContext);

        Assert.Equal(resultStr, result);
    }
}