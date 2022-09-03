using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class ParameterParserTests
{
    [Theory]
    [InlineData("\\\"132\\\"")]
    [InlineData("\\\"()(132)\\\"")]
    [InlineData("()(132)")]
    [InlineData("(132)")]
    [InlineData("()")]
    [InlineData(" ")]
    [InlineData("( )")]
    [InlineData("( )( )")]
    public void StringParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.StringParameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString().ToLower();
        Assert.Equal(parameterStr.ToLower(), str.ToLower());
    }
    
    [Theory]
    [InlineData("\"132\"")]
    [InlineData("( ")]
    [InlineData("( )(")]
    public void StringParameterParsingShouldBeFail(string str)
    {
        Assert.Throws<ParseException>(() =>
        {
            var result = ParametersParser.StringParameter.Before(CommonParser.EOF).ParseOrThrow(str);
            return result;
        });
    }
    
    [Theory]
    [InlineData("\"132\"")]
    [InlineData("\"132$var1$\"")]
    [InlineData("\"132 $var1$\"")]
    [InlineData("\"132 $var1$ \"")]
    [InlineData("\"123$var1$.Lenght456\"")]
    [InlineData("\" \\\"132\\\" \"")]
    [InlineData("\" \"")]
    public void StringFormatParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.StringFormatParameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString().ToLower();
        Assert.Equal(parameterStr.ToLower(), str.ToLower());
    }
    
    [Theory]
    [InlineData("\\\"132\\\"")]
    public void StringFormatParameterParsingShouldBeFail(string str)
    {
        Assert.Throws<ParseException>(() => ParametersParser.StringFormatParameter.ParseOrThrow(str));
    }
    
    [Theory]
    [InlineData("$var$")]
    [InlineData("$var$.Trim")]
    [InlineData("$var$.Lenght")]
    [InlineData("$var$.Lenght.Trim")]
    [InlineData("$var$.RemoveSubStr(\"123\")")]
    [InlineData("$var$.Lenght.RemoveSubStr(\"123\")")]
    [InlineData("$var$.Lenght.Trim.RemoveSubStr(\"123\")")]
    public void ParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.Parameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString().ToLower();
        Assert.Equal(parameterStr.ToLower(), str.ToLower());
    }
    
}