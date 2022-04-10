using System;
using System.Text.RegularExpressions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class UnarySubRule : IRule
    {
        public SubRuleType Type { get; }
        
        public IRuleParameter Parameter { get; }
        
        public UnarySubRule(SubRuleType type, IRuleParameter parameter)
        {
            Type = type;
            Parameter = parameter;
        }

        public bool Execute(string value)
        {
            var param = Parameter.GetValue();
            
            return Type switch
            {
                SubRuleType.Equals => value.Equals(param),
                SubRuleType.Contains => value.Contains(param),
                SubRuleType.StartsWith => value.StartsWith(param),
                SubRuleType.EndsWith => value.EndsWith(param),
                SubRuleType.Match => Regex.IsMatch(value, param),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{Type}{Constant.Space}{Parameter}";
        } 
    }
}