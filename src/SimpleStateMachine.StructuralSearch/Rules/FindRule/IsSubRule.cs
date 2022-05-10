using System;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class IsSubRule : IRule
    {
        public PlaceholderType Argument { get; }
        
        public IRuleParameter Parameter { get; }
        
        public IsSubRule(IRuleParameter parameter, PlaceholderType argument)
        {
            Parameter = parameter;
            Argument = argument;
        }

        public bool Execute()
        {
            var value = Parameter.GetValue();
            
            return Argument switch
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
            return $"{Parameter}{Constant.Space}{SubRuleType.Is}{Constant.Space}{Argument}";
        }  
    }
}