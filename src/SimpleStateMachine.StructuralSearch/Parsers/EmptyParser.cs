﻿using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal static class EmptyParser
{
    public static readonly Parser<char, string> AlwaysCorrectString = new EmptyStringParser(true);
    public static readonly Parser<char, string> AlwaysNotCorrectString = new EmptyStringParser(false);
}