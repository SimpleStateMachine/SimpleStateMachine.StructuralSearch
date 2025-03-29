﻿using Pidgin;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class GrammarTests
{
    [Theory]
    [InlineData("123")]
    [InlineData("\\\"abc\\\"")]
    [InlineData("\\\"\\\"")]
    [InlineData("\\\\\\\"\\\\\\\"")]
    public static void StringLiteralParsingShouldBeSuccess(string input)
    {
        input = $"\"{input}\"";
        var result = Grammar.StringLiteral.Before(CommonParser.Eof).ParseOrThrow(input);
        result = $"\"{EscapeHelper.Escape(result, Constant.StringLiteralCharsToEscape)}\"";
        Assert.Equal(input, result);
    }
}