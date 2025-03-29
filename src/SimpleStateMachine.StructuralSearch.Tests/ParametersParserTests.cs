﻿using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Operator.String.Type;
using SimpleStateMachine.StructuralSearch.Parameters.Types;
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
    [InlineData("Input.Length")]
    [InlineData("Input.Extensions")]
    public static void PlaceholderInputParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderInput.Before(CommonParser.Eof).ParseOrThrow(str);
    }

    public static IEnumerable<string> PlaceholderPositionCases()
    {
        foreach (var position in Enum.GetNames<PlaceholderPositionType>())
        {
            foreach (var subProperty in Enum.GetNames<PlaceholderPositionSubProperty>())
                yield return $"{position}.{subProperty}";
        }
    }

    [Theory]
    [StringMemberData(nameof(PlaceholderPositionCases))]
    public static void PlaceholderPositionParsingShouldBeSuccess(string str)
    {
        ParametersParser.PlaceholderPosition.Before(CommonParser.Eof).ParseOrThrow(str);
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
        ParametersParser.ChainableString.Before(CommonParser.Eof).ParseOrThrow(str);
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
    // [InlineData("$var$.Column.Start")]
    // [InlineData("$var$.Column.End")]
    // [InlineData("$var$.Offset.Start")]
    // [InlineData("$var$.Offset.End")]
    // [InlineData("$var$.Line.Start")]
    // [InlineData("$var$.Line.End")]
    // [InlineData("$var$.Column.Start.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    // [InlineData("$var$.Column.End.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    // [InlineData("$var$.Offset.Start.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    // [InlineData("$var$.Offset.End.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    // [InlineData("$var$.Line.Start.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    // [InlineData("$var$.Line.End.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    // [InlineData("$var$.Trim")]
    // [InlineData("$var$.Trim.TrimEnd.TrimStart.ToUpper.ToLower")]
    public static void PropertyAccessParsingShouldBeSuccess(string str)
    {
        var parameter = ParametersParser.PropertyAccess.Before(CommonParser.Eof).ParseOrThrow(str);
        var result = parameter.ToString()!;
        Assert.Equal(result.ToLower(), str.ToLower());
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
        var parameter = ParametersParser.StringExpression.Before(CommonParser.Eof).ParseOrThrow(str);
        var result = parameter.ToString()!;
        Assert.Equal(result.ToLower(), str.ToLower());
    }
}