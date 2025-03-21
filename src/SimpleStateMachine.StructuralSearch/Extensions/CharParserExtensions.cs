using System;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions;

public static class CharParserExtensions
{
    public static Parser<TToken, string> AsString<TToken>(this Parser<TToken, char> parser) 
        => parser.Select(x => x.ToString());

    public static Parser<TToken, string> AtLeastOnceAsStringUntil<TToken, U>(this Parser<TToken, char> parser, Parser<TToken, U> terminator) 
        => parser != null ? parser.AtLeastOnceUntil(terminator).AsString() : throw new ArgumentNullException(nameof (parser));

    public static Parser<TToken, string> AtLeastOnceAsStringUntilNot<TToken, U>(this Parser<TToken, char> parser, Parser<TToken, U> terminator) 
        => parser != null ? parser.AtLeastOnceUntilNot(terminator).AsString() : throw new ArgumentNullException(nameof (parser));
}