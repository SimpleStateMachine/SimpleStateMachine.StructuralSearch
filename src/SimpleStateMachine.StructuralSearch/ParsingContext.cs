using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public class ParsingContext : IParsingContext
    {
        public ParsingContext(IInput input)
        {
            Input = input;
        }
        public Dictionary<string, Placeholder> Placeholders { get; } = new();

        public IInput Input { get; }

        public bool TryGetPlaceholder(string name, out Placeholder value)
        {
            return Placeholders.TryGetValue(name, out value);
        }

        public void AddPlaceholder(Placeholder placeholder)
        {
            Placeholders[placeholder.Name] = placeholder;
        }

        public Placeholder GetPlaceholder(string name)
        {
            return Placeholders[name];
        }
    }
}