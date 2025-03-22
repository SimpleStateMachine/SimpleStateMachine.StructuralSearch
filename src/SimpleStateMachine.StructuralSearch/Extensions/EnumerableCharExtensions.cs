using System.Collections.Generic;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class EnumerableCharExtensions
{
    public static Parser<TToken, string> AsString<TToken>(this Parser<TToken, IEnumerable<char>> parser) 
        => parser.Select(x => new string(x.ToArray()));

    public static Parser<TToken, IEnumerable<T>> MergerMany<TToken, T>(this Parser<TToken, IEnumerable<IEnumerable<T>>> parser) 
        => parser.Select(x => x.SelectMany(y => y));

    public static Parser<TToken, IEnumerable<T>> MergerMany<TToken, T>(this Parser<TToken, IEnumerable<List<T>>> parser) 
        => parser.Select(x => x.SelectMany(y => y));
}