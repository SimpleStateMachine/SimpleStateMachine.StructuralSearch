using System;
using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.String.Type;
using SimpleStateMachine.StructuralSearch.Parameters.Types;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing;

public static class ParametersParserTests
{
    [Theory]
    [InlineData("$var$")]
    public static void PlaceholderParsingShouldBeSuccess(string str)
    {
        var result = ParametersParser.PlaceholderParameter.ParseToEnd(str);
        Assert.Equal(str, result.ToString(), true);
    }

    [Theory]
    [InlineData("Length")]
    public static void PlaceholderLengthParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderLength.ParseToEnd(str);
    }

    [Theory]
    [InlineData("Input.Length")]
    [InlineData("Input.Extensions")]
    public static void PlaceholderInputParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderInput.ParseToEnd(str);
    }

    public static IEnumerable<string> PlaceholderPositionCases()
    {
        foreach (var position in Enum.GetNames<PlaceholderPositionType>())
        foreach (var subProperty in Enum.GetNames<PlaceholderPositionSubProperty>())
            yield return $"{position}.{subProperty}";
    }

    [Theory]
    [StringMemberData(nameof(PlaceholderPositionCases))]
    public static void PlaceholderPositionParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderPosition.ParseToEnd(str);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("\\\"abc\\\"")]
    [InlineData("\\\"\\\"")]
    [InlineData("\\\\\\\"\\\\\\\"")]
    public static void StringLiteralParsingShouldBeSuccess(string input)
    {
        input = $"\"{input}\"";
        var parameter = ParametersParser.StringLiteral.ParseToEnd(input);
        var result = parameter.ToString();
        Assert.Equal(input, result);
    }

    public static IEnumerable<string> ChainableStringCases()
    {
        var template = ".{0}";
        var names = Enum.GetNames<StringUnaryOperator>().ToList();
        foreach (var position in names)
            yield return string.Format(template, position);

        yield return string.Format(template, string.Join('.', names));
    }

    [Theory]
    [StringMemberData(nameof(ChainableStringCases))]
    public static void ChainableStringParsingShouldBeSuccess(string str)
    {
        ParametersParser.ChainableString.ParseToEnd(str);
    }

    public static IEnumerable<string> PropertyAccessCases()
    {
        var placeholder = "$var$";
        foreach (var position in PlaceholderPositionCases())
        {
            yield return $"{placeholder}.{position}";

            foreach (var chainable in ChainableStringCases())
                yield return $"{placeholder}.{position}{chainable}";
        }

        foreach (var chainable in ChainableStringCases())
            yield return $"{placeholder}{chainable}";
    }

    [Theory]
    [StringMemberData(nameof(PropertyAccessCases))]
    public static void PropertyAccessParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.PropertyAccess.ParseToEnd(str);
        Assert.Equal(str, parameter.ToString(), true);
    }

    [Theory]
    [InlineData("\"132\"")]
    [InlineData("(\"132\")")]
    [InlineData("[(\"132\")]")]
    [InlineData("$var$")]
    [InlineData("$var$.Column.Start")]
    [InlineData("$var$.Column.End")]
    [InlineData("$var$.Offset.Start")]
    [InlineData("$var$.Offset.End")]
    [InlineData("$var$.Line.Start")]
    [InlineData("$var$.Line.End")]
    [InlineData("$var$.Trim")]
    [InlineData("$var$.Trim.Trim")]
    public static void StringExpressionParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.StringExpression.ParseToEnd(str);
        Assert.Equal(str.ToLower(), parameter.ToString(), true);
    }
}