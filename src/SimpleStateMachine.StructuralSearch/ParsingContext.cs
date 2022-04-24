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
        public readonly Dictionary<string, Placeholder> Placeholders = new();

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

        public IReadOnlyDictionary<string, Placeholder> Switch()
        {
            var placeholders = Placeholders
                .OrderBy(x=> x.Value.Offset.Start)
                .ToDictionary(x=> x.Key, x=> x.Value);
            Placeholders.Clear();
            return placeholders;
        }
    }
}