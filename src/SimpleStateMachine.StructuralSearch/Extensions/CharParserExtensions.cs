using System;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class CharParserExtensions
{
    public static Parser<TToken, string> AsString<TToken>(this Parser<TToken, char> parser) 
        => parser.Select(x => x.ToString());

    public static Parser<TToken, string> AtLeastOnceAsStringUntil<TToken, TTerminator>(this Parser<TToken, char> parser, Parser<TToken, TTerminator> terminator) 
        => parser != null ? parser.AtLeastOnceUntil(terminator).Select(x => new string(x.ToArray())) : throw new ArgumentNullException(nameof (parser));
}