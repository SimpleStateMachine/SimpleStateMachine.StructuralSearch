using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public class ParsingContext
    {
        public Dictionary<string, string> Placeholders { get; } = new();

        public bool TryGet(string name, out string value)
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