﻿using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Templates.FindTemplate;

internal static class ParserToParser
{
    public static Parser<char, Parser<char, char>> CiChar(char token) 
        => Parser.CIChar(token).Select(Parser.CIChar);

    public static Parser<char, Parser<char, string>> ResultAsParser(Parser<char, string> parser, bool ignoreCase = false) 
        => parser.Select(value => Parsers.Parsers.String(value, ignoreCase));

    public static Parser<char, Parser<char, string>> ParserAsParser(Parser<char, string> parser) 
        => parser.Select(value => parser);

    public static Parser<char, Parser<char, SourceMatch>> ResultAsMatch(Parser<char, string> parser, bool ignoreCase = false) 
        => parser.Select(x=> Parsers.Parsers.String(x, ignoreCase).AsMatch());
}