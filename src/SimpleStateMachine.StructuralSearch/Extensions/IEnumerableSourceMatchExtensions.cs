using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class IEnumerableSourceMatchExtensions
    {
        public static SourceMatch Concatenate(this IEnumerable<SourceMatch> matches)
        {
            var sourceMatches = matches as SourceMatch[] ?? matches.ToArray();
            int start = sourceMatches.First().Start;
            int end = sourceMatches.Last().End;
            var value = string.Join(string.Empty, sourceMatches.Select(x => x.Value));
            return new SourceMatch(value, start, end);
        }
    }
}