using System.Collections.Generic;
using System.Reflection.Metadata;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ReplaceRule
    {
        public IRule FindRule { get; }
        
        public IEnumerable<ReplaceSubRule> Rules { get; }
        
        public ReplaceRule(IRule findRule, IEnumerable<ReplaceSubRule> rules)
        {
            FindRule = findRule;
            Rules = rules;
        }
        
        public override string ToString()
        {
            return $"{FindRule}{Constant.Space}{Constant.Then}{Constant.Space}{string.Join(Constant.Comma, Rules)}";
        } 
    }
}