using Pidgin;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ParametersParserTests
{
    [Theory]
    [InlineData("$var$")]
    public static void PlaceholderParsingShouldBeSuccess(string str)
    {
        var result = ParametersParser.PlaceholderParameter.Before(CommonParser.Eof).ParseOrThrow(str);
        // result = EscapeHelper.Escape(result);
        // Assert.Equal(result.ToLower(), str.ToLower());
    }
    
    [Theory]
    [InlineData("Length")]
    public static void PlaceholderLengthParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderLength.Before(CommonParser.Eof).ParseOrThrow(str);
    }
    
    [Theory]
    [InlineData("Input.Lenght")]
    [InlineData("Input.Extensions")]
    public static void PlaceholderInputParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderInput.Before(CommonParser.Eof).ParseOrThrow(str);
    }

    [Theory]
    [InlineData("Column.Start")]
    [InlineData("Column.End")]
    [InlineData("Offset.Start")]
    [InlineData("Offset.End")]
    [InlineData("Line.Start")]
    [InlineData("Line.End")]
    public static void PlaceholderPositionParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderPosition.Before(CommonParser.Eof).ParseOrThrow(str);
    }

    [Theory]
    [InlineData(".Trim")]
    [InlineData(".Trim.Trim.Trim")]
    [InlineData(".Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    public static void ChainableStringParsingShouldBeSuccess(string str)
    {
        ParametersParser.ChainableString.Before(CommonParser.Eof).ParseOrThrow(str);
    }
    
    [Theory]
    [InlineData("$var$.Column.Start")]
    [InlineData("$var$.Column.End")]
    [InlineData("$var$.Offset.Start")]
    [InlineData("$var$.Offset.End")]
    [InlineData("$var$.Line.Start")]
    [InlineData("$var$.Line.End")]
    [InlineData("$var$.Column.Start.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    [InlineData("$var$.Column.End.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    [InlineData("$var$.Offset.Start.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    [InlineData("$var$.Offset.End.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    [InlineData("$var$.Line.Start.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    [InlineData("$var$.Line.End.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    [InlineData("$var$.Trim")]
    [InlineData("$var$.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    public static void PropertyAccessParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.PropertyAccess.Before(CommonParser.Eof).ParseOrThrow(str);
        var result = parameter.ToString()!;
        Assert.Equal(result.ToLower(), str.ToLower());
    }
    
    [Theory]
    [InlineData("132")]
    [InlineData(" ")]
    [InlineData("(132)")]
    [InlineData("[(132)]")]
    [InlineData("( )")]
    [InlineData("$var$")]
    [InlineData("$var$.Column.Start")]
    [InlineData("$var$.Column.End")]
    [InlineData("$var$.Offset.Start")]
    [InlineData("$var$.Offset.End")]
    [InlineData("$var$.Line.Start")]
    [InlineData("$var$.Line.End")]
    [InlineData("$var$.Trim")]
    [InlineData("$var$.Trim.Trim")]
    [InlineData("( )( )")]
    public static void StringExpressionParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.StringExpression.Before(CommonParser.Eof).ParseOrThrow(str);
        var result = parameter.ToString()!;
        Assert.Equal(result.ToLower(), str.ToLower());
    }
    
    [Theory]
    [InlineData("$var$.Column")]
    [InlineData("$var$.Offset")]
    [InlineData("$var$.Line")]
    [InlineData("$var$.Lenght.Trim")]
    [InlineData("$var$.Lenght.RemoveSubStr(\"123\")")]
    [InlineData("$var$.Lenght.Trim.RemoveSubStr(\"123\")")]
    public static void StringExpressionParsingShouldBeFailed(string str)
    {
        Assert.Throws<ParseException<char>>(() => ParametersParser.StringExpression.Before(CommonParser.Eof).ParseOrThrow(str));
    }
}