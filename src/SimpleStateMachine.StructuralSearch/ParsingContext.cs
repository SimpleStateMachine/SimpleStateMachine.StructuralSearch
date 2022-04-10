using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public class ParsingContext : IParsingContext
    {
        public Dictionary<string, Placeholder.Placeholder?> Placeholders { get; } = new();

        public bool TryGetPlaceholder(string name, out Placeholder.Placeholder? value)
        {
            return Placeholders.TryGetValue(name, out value);
        }

        public void AddPlaceholder(string name, string value)
        {
            Placeholders[name] = new Placeholder.Placeholder(this, name, value);
        }

        public Placeholder.Placeholder? GetPlaceholder(string name)
        {
            return Placeholders[name];
        }
    }
}