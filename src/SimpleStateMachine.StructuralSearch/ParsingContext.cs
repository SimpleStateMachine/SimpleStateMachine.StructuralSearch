using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch
{
    public class ParsingContext : IParsingContext
    {
        public static readonly EmptyParsingContext Empty = new (SimpleStateMachine.StructuralSearch.Input.Empty);

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
        
        public void Fill(IReadOnlyDictionary<string, IPlaceholder> placeholders)
        {
            ClearInternal();
            
            foreach (var placeholder in placeholders)
            {
                Placeholders.Add(placeholder.Key, placeholder.Value);
            }
        }

        public IReadOnlyDictionary<string, IPlaceholder> Clear()
        {
            var placeholders = Placeholders
                .OrderBy(x=> x.Value.Offset.Start)
                .ToDictionary(x=> x.Key, x=> x.Value);
            
            ClearInternal();
            
            return placeholders;
        }

        private void ClearInternal()
        {
            Placeholders.Clear();;
        }
    }
}