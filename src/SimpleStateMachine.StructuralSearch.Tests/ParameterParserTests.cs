using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class ParameterParserTests
{
    [Theory]
    [InlineData("\\\"132\\\"")]
    public void StringParameterParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.StringParameter.ParseOrThrow(str);
        var parameterStr = parameter.ToString().ToLower();
        Assert.Equal(parameterStr.ToLower(), str.ToLower());
    }
    
    [Theory]
    [InlineData("\"132\"")]
    public void StringParameterParsingShouldBeFail(string str)
    {
        Assert.Throws<ParseException>(() => ParametersParser.StringParameter.ParseOrThrow(str));
    }
    
    [Theory]
    [InlineData("\"132\"")]
    [InlineData("\"132$var1$\"")]
    [InlineData("\"132 $var1$\"")]
    [InlineData("\"132 $var1$ \"")]
    [InlineData("\"123$var1$.Lenght456\"")]
    [InlineData("\" \\\"132\\\" \"")]
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
}