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
        var result = ParametersParser.StringExpression.Before(CommonParser.Eof).ParseOrThrow(str);
        // result = EscapeHelper.Escape(result);
        // Assert.Equal(result.ToLower(), str.ToLower());
    }

    // [Theory]
    // [InlineData("(\\\"132\\\")")]
    // [InlineData("(132)")]
    // [InlineData("[(132)]")]
    // [InlineData("{(())}")]
    // [InlineData("()")]
    // [InlineData("( )")]
    // public static void StringInParenthesesParsingShouldBeSuccess(string str)
    // {
    //     var result = StringParameterParser.StringInParentheses.Before(CommonParser.Eof).ParseOrThrow(str);
    //     result = EscapeHelper.Escape(result);
    //     Assert.Equal(result.ToLower(), str.ToLower());
    // }
    //
    // [Theory]
    // [InlineData("(\\\"132\\\")")]
    // [InlineData("\\\"132\\\"")]
    // [InlineData("\\\"()(132)\\\"")]
    // [InlineData("()(132)")]
    // [InlineData("(132)")]
    // [InlineData("()")]
    // [InlineData(" ")]
    // [InlineData("( )")]
    // [InlineData("( )( )")]
    // [InlineData("\"132\"")]
    // public static void StringParameterParsingShouldBeSuccess(string str)
    // {
    //     var parameter = StringParameterParser.StringParameter.Before(CommonParser.Eof).ParseOrThrow(str);
    //     var parameterStr = parameter.ToString()?.ToLower();
    //     Assert.Equal(str.ToLower(), parameterStr?.ToLower());
    // }
    //
    // [Theory]
    // [InlineData("( ")]
    // [InlineData("( )(")]
    // public static void StringParameterParsingShouldBeFail(string str)
    // {
    //     Assert.Throws<ParseException<char>>(() =>
    //     {
    //         var result = StringParameterParser.StringParameter.Before(CommonParser.Eof).ParseOrThrow(str);
    //         return result;
    //     });
    // }
    //
    // [Theory]
    // [InlineData("$var$")]
    // public static void PlaceholderParameterParsingShouldBeSuccess(string str)
    // {
    //     var parameter = ParametersParser.PlaceholderParameter.Before(CommonParser.Eof).ParseOrThrow(str);
    //     var parameterStr = parameter.ToString().ToLower();
    //     Assert.Equal(str.ToLower(), parameterStr.ToLower());
    // }
    //
    // [Theory]
    // [InlineData("\"132\"")]
    // [InlineData("\"132$var1$\"")]
    // // [InlineData("\"132 $var1$\"")]
    // // [InlineData("\"132 $var1$ \"")]
    // [InlineData("\"123$var1$.Lenght456\"")]
    // // [InlineData("\" \\\"132\\\" \"")]
    // // [InlineData("\" \"")]
    // public static void StringFormatParameterParsingShouldBeSuccess(string str)
    // {
    //     var parameter = StringParameterParser.StringFormatParameter.ParseOrThrow(str);
    //     var parameterStr = parameter.ToString()?.ToLower();
    //     Assert.Equal(parameterStr?.ToLower(), str.ToLower());
    // }
    //
    // [Theory]
    // [InlineData("\\\"132\\\"")]
    // public static void StringFormatParameterParsingShouldBeFail(string str)
    // {
    //     Assert.Throws<ParseException<char>>(() => StringParameterParser.StringFormatParameter.ParseOrThrow(str));
    // }
    //
    // [Theory]
    // [InlineData("$var$")]
    // [InlineData("$var$.Lenght")]
    // [InlineData("$var$.Column.Start")]
    // [InlineData("$var$.Column.End")]
    // [InlineData("$var$.Offset.Start")]
    // [InlineData("$var$.Offset.End")]
    // [InlineData("$var$.Line.Start")]
    // [InlineData("$var$.Line.End")]
    // [InlineData("$var$.Trim")]
    // [InlineData("$var$.Trim.Trim")]
    // public static void ParameterParsingShouldBeSuccess(string str)
    // {
    //     var parameter = ParametersParser.Parameter.Before(CommonParser.Eof).ParseOrThrow(str);
    //     var parameterStr = parameter.ToString()?.ToLower();
    //     Assert.Equal(str.ToLower(), parameterStr?.ToLower());
    // }
    //
    // [Theory]
    // [InlineData("$var$.Column")]
    // [InlineData("$var$.Offset")]
    // [InlineData("$var$.Line")]
    // [InlineData("$var$.Lenght.Trim")]
    // [InlineData("$var$.Lenght.RemoveSubStr(\"123\")")]
    // [InlineData("$var$.Lenght.Trim.RemoveSubStr(\"123\")")]
    // public static void ParameterParsingShouldBeFailed(string str)
    // {
    //     Assert.Throws<ParseException<char>>(() =>
    //     {
    //         var t = ParametersParser.Parameter.Before(CommonParser.Eof).ParseOrThrow(str);
    //     });
    // }
}