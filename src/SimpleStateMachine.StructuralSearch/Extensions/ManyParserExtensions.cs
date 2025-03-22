using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class ManyParserExtensions
{
    internal static Parser<TToken, IEnumerable<T>> ToIEnumerable<TToken, T>(this Parser<TToken, List<T>> parser) 
        => parser.As<TToken, List<T>, IEnumerable<T>>();
}