using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public class ParsingContext
    {
        public Dictionary<string, string> Placeholders { get; } = new();

        public bool TryAdd(string name, string value)
        {
            if (Placeholders.ContainsKey(name))
                return Placeholders[name] == value;

            Placeholders.Add(name, value);
            
            return true;
        }
        
        public string GetPlaceholder(string name)
        {
            return Placeholders[name];
        }
    }
}