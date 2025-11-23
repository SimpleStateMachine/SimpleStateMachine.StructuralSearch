using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Replace;

public class ReplaceResult(
    FindParserResult findResult,
    IReadOnlyDictionary<string, IPlaceholder> newPlaceholders,
    string newValue)
{
    public FindParserResult FindResult { get; } = findResult;
    public IReadOnlyDictionary<string, IPlaceholder> NewPlaceholders { get; } = newPlaceholders;
    public string Value { get; } = newValue;
}