using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Extensions
{
    public static class IEnumerableSourceMatchExtensions
    {
        public static SourceMatch Concatenate(this IEnumerable<SourceMatch> matches)
        {
            int start = matches.First().Start;
            int end = matches.Last().End;
            var value = string.Join(string.Empty, matches.Select(x => x.Value));
            return new SourceMatch(value, start, end);
        }
    }
}