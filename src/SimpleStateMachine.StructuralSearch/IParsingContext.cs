using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        IInput Input { get; }
        
        bool TryGetPlaceholder(string name, out IPlaceholder value);
        void AddPlaceholder(IPlaceholder placeholder);
        void RemovePlaceholder(IPlaceholder placeholder);
        IPlaceholder GetPlaceholder(string name);
        
        void Fill(IReadOnlyDictionary<string, IPlaceholder>placeholders);
        IReadOnlyDictionary<string, IPlaceholder> Clear();
    }
}