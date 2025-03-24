using Pidgin;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ParameterParserTests
{
    [Theory]
    [InlineData("\\\"132\\\"")]
    [InlineData("132")]
    [InlineData(" ")]
    public static void OptionalStringParsingShouldBeSuccess(string str)
    {
        var result = ParametersParser.String.ParseOrThrow(str);
        result = EscapeHelper.Escape(result);
        Assert.Equal(result.ToLower(), str.ToLower());
    }

    [Theory]
    [InlineData("(\\\"132\\\")")]
    [InlineData("(132)")]
    [InlineData("[(132)]")]
    [InlineData("{(())}")]
    [InlineData("()")]
    [InlineData("( )")]
    public static void StringInParenthesesParsingShouldBeSuccess(string str)
    {
        var result = ParametersParser.StringInParentheses.ParseOrThrow(str);
        result = EscapeHelper.Escape(result);
        Assert.Equal(result.ToLower(), str.ToLower());
    }

    [Theory]
    [InlineData("(\\\"132\\\")")]
    [InlineData("\\\"132\\\"")]
    [InlineData("\\\"()(132)\\\"")]
    [InlineData("()(132)")]
    [InlineData("(132)")]
    [InlineData("()")]
    [InlineData(" ")]
    [InlineData("( )")]
    [InlineData("( )( )")]
    public static void StringParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.StringParameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString()?.ToLower();
        Assert.Equal(parameterStr?.ToLower(), str.ToLower());
    }

    [Theory]
    [InlineData("\"132\"")]
    [InlineData("( ")]
    [InlineData("( )(")]
    public static void StringParameterParsingShouldBeFail(string str)
    {
        Assert.Throws<ParseException<char>>(() =>
        {
            var result = ParametersParser.StringParameter.Before(CommonParser.Eof).ParseOrThrow(str);
            return result;
        });
    }

    [Theory]
    [InlineData("$var$")]
    public static void PlaceholderParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.PlaceholderParameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString().ToLower();
        Assert.Equal(parameterStr.ToLower(), str.ToLower());
    }

    [Theory]
    [InlineData("\"132\"")]
    [InlineData("\"132$var1$\"")]
    [InlineData("\"132 $var1$\"")]
    [InlineData("\"132 $var1$ \"")]
    [InlineData("\"123$var1$.Lenght456\"")]
    [InlineData("\" \\\"132\\\" \"")]
    [InlineData("\" \"")]
    public static void StringFormatParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.StringFormatParameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString()?.ToLower();
        Assert.Equal(parameterStr?.ToLower(), str.ToLower());
    }

    [Theory]
    [InlineData("\\\"132\\\"")]
    public static void StringFormatParameterParsingShouldBeFail(string str)
    {
        Assert.Throws<ParseException<char>>(() => ParametersParser.StringFormatParameter.ParseOrThrow(str));
    }

    [Theory]
    [InlineData("$var$")]
    [InlineData("$var$.Trim")]
    [InlineData("$var$.Lenght")]
    [InlineData("$var$.Lenght.Trim")]
    [InlineData("$var$.RemoveSubStr(\"123\")")]
    [InlineData("$var$.Lenght.RemoveSubStr(\"123\")")]
    [InlineData("$var$.Lenght.Trim.RemoveSubStr(\"123\")")]
    public static void ParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.Parameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString()?.ToLower();
        Assert.Equal(parameterStr?.ToLower(), str.ToLower());
    }
}