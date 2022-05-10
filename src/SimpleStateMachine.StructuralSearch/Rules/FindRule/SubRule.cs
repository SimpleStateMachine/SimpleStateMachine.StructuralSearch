using System;
using System.Text.RegularExpressions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class SubRule : IRule
    {
        public SubRuleType Type { get; }
        
        public IRuleParameter Left { get; }
        
        public IRuleParameter Right { get; }
        
        public SubRule(SubRuleType type, IRuleParameter left, IRuleParameter right)
        {
            Type = type;
            Left = left;
            Right = right;
        }

        public bool Execute()
        {
            var left = Right.GetValue();
            var right = Left.GetValue();
            
            return Type switch
            {
                SubRuleType.Equals => left.Equals(right),
                SubRuleType.Contains => left.Contains(right),
                SubRuleType.StartsWith => left.StartsWith(right),
                SubRuleType.EndsWith => left.EndsWith(right),
                SubRuleType.Match => Regex.IsMatch(left, right),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{Left}{Constant.Space}{Type}{Constant.Space}{Right}";
        } 
    }
}