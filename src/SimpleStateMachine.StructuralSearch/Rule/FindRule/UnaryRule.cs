using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class UnaryRule : IRule
    {
        public UnaryRuleType Type { get; }

        public IRule Parameter { get; }

        public UnaryRule(UnaryRuleType type, IRule parameter)
        {
            Type = type;
            Parameter = parameter;
        }

        public bool Execute(string value)
        {
            var result = Parameter.Execute(value);
            
            return Type switch
            {
                UnaryRuleType.Not => !result,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{Type} {Parameter}";
        } 
    }
}