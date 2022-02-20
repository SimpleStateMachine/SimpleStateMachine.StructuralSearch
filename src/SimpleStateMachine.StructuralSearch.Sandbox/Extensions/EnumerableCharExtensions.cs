using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Extensions
{
    public static class EnumerableCharExtensions
    {
        public static Parser<Ttoken, string> AsString<Ttoken>(this Parser<Ttoken, IEnumerable<char>> parser)
        {
            return parser.Select(x => new string(x.ToArray()));
        }
    }
}