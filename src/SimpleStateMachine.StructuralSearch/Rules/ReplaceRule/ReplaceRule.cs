using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRule
{
    public class ReplaceRule
    {
        public FindRule.FindRule FindRule { get; }

        public IRuleParameter Parameter { get; }

        public ReplaceRule(FindRule.FindRule findRule, IRuleParameter parameter)
        {
            FindRule = findRule;
            Parameter = parameter;
        }
        
        public override string ToString()
        {
            return $"{FindRule}{Constant.Space}{Constant.Should}{Constant.Space}{Parameter}";
        } 
    }
}