using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class ManyParserExtensions
    {
        public static Parser<TToken, IEnumerable<T>> ToIEnumerable<TToken, T>(this Parser<TToken, List<T>> parser)
        {
            return parser.As<TToken, List<T>, IEnumerable<T>>();
        }
    }
}