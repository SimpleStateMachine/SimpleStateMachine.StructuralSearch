using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public class ParsingContext : IParsingContext
    {
        public Dictionary<string, string> Placeholders { get; } = new();

        public bool TryGetPlaceholder(string name, out string value)
        {
            return Placeholders.TryGetValue(name, out value);
        }

        public void AddPlaceholder(string name, string value)
        {
            Placeholders[name] = value;
        }

        public string GetPlaceholder(string name)
        {
            return Placeholders[name];
        }
    }
}