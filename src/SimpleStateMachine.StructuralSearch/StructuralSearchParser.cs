
using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.Rules.FindRule;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRule;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch
{
    public class StructuralSearchParser
    {
        public readonly IFindParser FindTemlate;

        public readonly IEnumerable<FindRule> FindRules;

        public readonly IReplaceBuilder ReplaceTemplate;

        public readonly IEnumerable<ReplaceRule> ReplaceRules;
        
        public StructuralSearchParser(Configuration configuration)
        {
            FindTemlate = FindTemplateParser.ParseTemplate(configuration.FindTemplate);
            FindRules = configuration.FindRules.Select(FindRuleParser.ParseTemplate);
            ReplaceTemplate = ReplaceTemplateParser.ParseTemplate(configuration.ReplaceTemplate);
            ReplaceRules = configuration.ReplaceRules.Select(ReplaceRuleParser.ParseTemplate);
        }
    }
}