using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ReplaceRule
    {
        public FindRule FindRule { get; }

        public IRuleParameter Parameter { get; }

        public ReplaceRule(FindRule findRule, IRuleParameter parameter)
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