using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Replace;

public class ReplaceResult
{
    public ReplaceResult(FindParserResult findResult, IReadOnlyDictionary<string, IPlaceholder> newPlaceholders, string newValue)
    {
        FindResult = findResult;
        NewPlaceholders = newPlaceholders;
        Value = newValue;
    }

    public FindParserResult FindResult { get; }
    public IReadOnlyDictionary<string, IPlaceholder> NewPlaceholders { get; }
    public string Value { get; }
}