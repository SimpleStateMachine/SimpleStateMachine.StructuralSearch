using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch;

public readonly record struct FindParserMatch(Match<string> Match, IReadOnlyDictionary<string, Placeholder> Placeholders);