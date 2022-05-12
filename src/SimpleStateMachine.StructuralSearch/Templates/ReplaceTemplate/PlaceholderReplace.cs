﻿namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate
{
    public class PlaceholderReplace : IReplaceStep, IContextDependent
    {
        private IParsingContext _context;
        
        public string Name { get; }

        public PlaceholderReplace(string name)
        {
            Name = name;
        }

        public string GetValue()
        {
            return _context.GetPlaceholder(Name).Value;
        }

        public void SetContext(ref IParsingContext context)
        {
            _context = context;
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
        }  
    }
}