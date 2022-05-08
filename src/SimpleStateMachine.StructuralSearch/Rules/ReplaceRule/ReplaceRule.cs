using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ReplaceRule
    {
        public IRule FindRule { get; }
        public PlaceholderParameter Placeholder { get; }
        public IRuleParameter Parameter { get; }
        
        public ReplaceRule(IRule findRule, PlaceholderParameter placeholder, IRuleParameter parameter)
        {
            FindRule = findRule;
            Placeholder = placeholder;
            Parameter = parameter;
        }
        
        public override string ToString()
        {
            return $"{FindRule}{Constant.Space}{Constant.Should}{Constant.Space}{Parameter}";
        } 
    }
}