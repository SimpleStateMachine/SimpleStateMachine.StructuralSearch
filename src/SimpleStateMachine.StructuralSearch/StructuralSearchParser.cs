
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class StructuralSearchParser
    {
        public IFindParser FindParser { get; set; }

        public List<FindRule> FindRules { get; set; }

        public IReplaceBuilder ReplaceBuilder { get; set; }

        public List<ReplaceRule> ReplaceRules { get; set; }
        
        public StructuralSearchParser(Configuration configuration)
        {
            
        }
    }
}