using System;
using System.Text.RegularExpressions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class BinarySubRule : IRule
    {
        private readonly SubRuleType _type;
        private readonly IRuleParameter _left;
        private readonly IRuleParameter _right;
        
        public BinarySubRule(SubRuleType type, IRuleParameter left, IRuleParameter right)
        {
            _type = type;
            _left = left;
            _right = right;
        }

        public bool Execute()
        {
            var left = _left.GetValue();
            var right = _right.GetValue();
            
            return _type switch
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
            return $"{_left}{Constant.Space}{_type}{Constant.Space}{_right}";
        }

        public void SetContext(ref IParsingContext context)
        {
            _left.SetContext(ref context);
            _right.SetContext(ref context);
        }
    }
}