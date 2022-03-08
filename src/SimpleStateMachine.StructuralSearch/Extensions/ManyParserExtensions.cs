using System.Collections.Generic;
using Pidgin;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class ManyParserExtensions
    {
        public static Parser<TToken, IEnumerable<T>> ToIEnumerable<TToken, T>(this Parser<TToken, List<T>> parser)
        {
            return parser.Cast<IEnumerable<T>>();
        }
    }
}