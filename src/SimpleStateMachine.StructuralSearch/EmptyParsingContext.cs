﻿using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public class EmptyParsingContext : IParsingContext
    {
        public IInput Input { get; }
        
        public EmptyParsingContext(IInput input)
        {
            Input = input;
        }

        public bool TryGetPlaceholder(string name, out IPlaceholder value)
            => throw new System.NotImplementedException();

        public void AddPlaceholder(IPlaceholder placeholder) 
            => throw new System.NotImplementedException();

        public IPlaceholder GetPlaceholder(string name) 
            => Placeholder.CreateEmpty(this, name, string.Empty);

        public IReadOnlyDictionary<string, IPlaceholder> SwitchOnNew() 
            => throw new System.NotImplementedException();

        public void Fill(IReadOnlyDictionary<string, IPlaceholder> placeholders) 
            => throw new System.NotImplementedException();

        public IReadOnlyDictionary<string, IPlaceholder> Clear()
            => throw new System.NotImplementedException();
    }
}