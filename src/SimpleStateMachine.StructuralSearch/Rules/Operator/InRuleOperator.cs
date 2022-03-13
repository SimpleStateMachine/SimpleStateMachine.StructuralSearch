using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class InRuleOperator : IRuleOperator
    {
        public RuleOperatorType Type { get; }
        
        public IEnumerable<string> Values { get; }
        
        public InRuleOperator(RuleOperatorType type, IEnumerable<string> values)
        {
            Type = type;
            
            Values = values;
        }
    }
}