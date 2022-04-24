using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IParsingContext
    {
        IInput Input { get; }
        
        bool TryGetPlaceholder(string name, out Placeholder value);

        void AddPlaceholder(Placeholder placeholder);

        Placeholder GetPlaceholder(string name);
        
        IReadOnlyDictionary<string, Placeholder> Switch();
    }
}