using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Placeholder;

namespace SimpleStateMachine.StructuralSearch;

public readonly record struct FindParserResult(Match<string> Match, IReadOnlyDictionary<string, IPlaceholder> Placeholders);