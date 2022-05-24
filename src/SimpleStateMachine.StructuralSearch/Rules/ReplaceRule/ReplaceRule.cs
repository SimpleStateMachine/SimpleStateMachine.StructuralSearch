using System.Collections.Generic;
using System.Reflection.Metadata;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ReplaceRule
    {
        public IRule ConditionRule { get; }
        
        public IEnumerable<ReplaceSubRule> Rules { get; }
        
        public ReplaceRule(IRule conditionRule, IEnumerable<ReplaceSubRule> rules)
        {
            ConditionRule = conditionRule;
            Rules = rules;
        }
        
        public override string ToString()
        {
            return $"{ConditionRule}{Constant.Space}{Constant.Then}{Constant.Space}{string.Join(Constant.Comma, Rules)}";
        } 
    }
}