using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch;

public readonly record struct FindParserResult(Match<string> Match, IReadOnlyDictionary<string, IPlaceholder> Placeholders);