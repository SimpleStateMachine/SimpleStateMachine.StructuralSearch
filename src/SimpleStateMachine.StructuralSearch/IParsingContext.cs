using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        IInput Input { get; }
        
        bool TryGetPlaceholder(string name, out IPlaceholder value);

        void AddPlaceholder(IPlaceholder placeholder);

        IPlaceholder GetPlaceholder(string name);
        
        IReadOnlyDictionary<string, IPlaceholder> SwitchOnNew();
        void Set(IReadOnlyDictionary<string, IPlaceholder>placeholders);
        void Clear();
    }
}