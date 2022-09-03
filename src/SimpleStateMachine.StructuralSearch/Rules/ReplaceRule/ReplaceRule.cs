using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ReplaceRule: IContextDependent
    {
        public readonly IRule ConditionRule;
        
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

        public void SetContext(ref IParsingContext context)
        {
            ConditionRule.SetContext(ref context);

            foreach (var rule in Rules)
            {
                rule.SetContext(ref context);
            }
        }
    }
}