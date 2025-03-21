using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch;

public class ParsingContext : IParsingContext
{
    public static readonly EmptyParsingContext Empty = new (SimpleStateMachine.StructuralSearch.Input.Empty);
    private readonly Dictionary<string, IPlaceholder> _placeholders = new();
    public IInput Input { get; }
        
    public ParsingContext(IInput input)
    {
        Input = input;
    }

    public bool TryGetPlaceholder(string name, out IPlaceholder value)
        => _placeholders.TryGetValue(name, out value!);

    public void AddPlaceholder(IPlaceholder placeholder)
    {
        _placeholders[placeholder.Name] = placeholder;
    }

    public void RemovePlaceholder(IPlaceholder placeholder)
    {
        _placeholders.Remove(placeholder.Name);
    }

    public IPlaceholder GetPlaceholder(string name) 
        => _placeholders[name];

    public void Fill(IReadOnlyDictionary<string, IPlaceholder> placeholders)
    {
        ClearInternal();
            
        foreach (var placeholder in placeholders)
        {
            _placeholders.Add(placeholder.Key, placeholder.Value);
        }
    }

    public IReadOnlyDictionary<string, IPlaceholder> Clear()
    {
        var placeholders = _placeholders
            .OrderBy(x=> x.Value.Offset.Start)
            .ToDictionary(x=> x.Key, x=> x.Value);
            
        ClearInternal();
            
        return placeholders;
    }

    private void ClearInternal() 
        => _placeholders.Clear();
}