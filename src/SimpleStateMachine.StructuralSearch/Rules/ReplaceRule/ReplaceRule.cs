using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ReplaceRule
    {
        public PlaceholderLogicalRule FindRule { get; }

        public IRuleParameter Parameter { get; }

        public ReplaceRule(PlaceholderLogicalRule findRule, IRuleParameter parameter)
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