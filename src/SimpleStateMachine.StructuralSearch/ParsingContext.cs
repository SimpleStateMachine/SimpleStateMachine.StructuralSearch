using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch
{
    public class ParsingContext : IParsingContext
    {
        public ParsingContext(IInput input)
        {
            Input = input;
        }
        public readonly Dictionary<string, IPlaceholder> Placeholders = new();

        public IInput Input { get; }

        public bool TryGetPlaceholder(string name, out IPlaceholder value)
        {
            return Placeholders.TryGetValue(name, out value);
        }

        public void AddPlaceholder(IPlaceholder placeholder)
        {
            Placeholders[placeholder.Name] = placeholder;
        }

        public IPlaceholder GetPlaceholder(string name)
        {
            return Placeholders[name];
        }

        public IReadOnlyDictionary<string, IPlaceholder> SwitchOnNew()
        {
            var placeholders = Placeholders
                .OrderBy(x=> x.Value.Offset.Start)
                .ToDictionary(x=> x.Key, x=> x.Value);
            Clear();
            return placeholders;
        }

        public void Set(IReadOnlyDictionary<string, IPlaceholder> placeholders)
        {
            Clear();
            
            foreach (var placeholder in placeholders)
            {
                Placeholders.Add(placeholder.Key, placeholder.Value);
            }
        }

        public void Clear()
        {
            Placeholders.Clear();
        }
    }
}