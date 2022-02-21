using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Extensions
{
    public static class CharParserExtensions
    {
        public static Parser<TToken, string> AsString<TToken>(this Parser<TToken, char> parser)
        {
            return parser.Select(x => x.ToString());
        }
    }
}