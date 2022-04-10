using System;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRule
{
    public class IsRule : IRule
    {
        public SubRuleType Type { get; }
        
        public PlaceholderType PlaceholderType { get; }
        
        public IsRule(SubRuleType type, PlaceholderType placeholderType)
        {
            Type = type;
            PlaceholderType = placeholderType;
        }

        public bool Execute(string value)
        {
            return PlaceholderType switch
            {
                PlaceholderType.Var => CommonParser.Identifier.TryParse(value, out _),
                PlaceholderType.Int => int.TryParse(value, out _),
                PlaceholderType.Double => double.TryParse(value, out _),
                PlaceholderType.DateTime => DateTime.TryParse(value, out _),
                PlaceholderType.Guid => Guid.TryParse(value, out _),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{Type}{Constant.Space}{PlaceholderType}";
        }  
    }
}