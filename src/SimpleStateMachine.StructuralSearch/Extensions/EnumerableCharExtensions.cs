using System.Collections.Generic;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class EnumerableCharExtensions
    {
        public static Parser<Ttoken, string> AsString<Ttoken>(this Parser<Ttoken, IEnumerable<char>> parser)
        {
            return parser.Select(x => new string(x.ToArray()));
        }

        public static Parser<Ttoken, IEnumerable<T>> MergerMany<Ttoken, T>(
            this Parser<Ttoken, IEnumerable<IEnumerable<T>>> parser)
        {
            return parser.Select(x => x.SelectMany(y => y));
        }
        
        public static Parser<Ttoken, IEnumerable<T>> MergerMany<Ttoken, T>(
            this Parser<Ttoken, IEnumerable<List<T>>> parser)
        {
            return parser.Select(x => x.SelectMany(y => y));
        }
    }
}