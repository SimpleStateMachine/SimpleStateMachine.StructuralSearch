
using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class StructuralSearchParser
    {
        public IFindParser FindParser { get; set; }

        public Dictionary<string, FindRule> FindRules { get; set; }

        public IReplaceBuilder ReplaceBuilder { get; set; }

        public Dictionary<string, ReplaceRule> ReplaceRules { get; set; }
        
        public StructuralSearchParser(Configuration configuration)
        {
            FindParser = StructuralSearch.ParseFindTemplate(configuration.FindTemplate);
            
            FindRules = configuration.FindRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseFindRule)
                .ToDictionary(x => x.Placeholder.Name, x => x);
            
            ReplaceBuilder = StructuralSearch.ParseReplaceTemplate(configuration.ReplaceTemplate);
            
            ReplaceRules = configuration.ReplaceRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseReplaceRule)
                .ToDictionary(x => x.FindRule.Placeholder.Name, x => x);
        }

        public void Parse(ref IParsingContext context)
        {
            FindParser.Parse(ref context, context.File.Data);
        }
        
        public void Replace(ref IParsingContext context)
        {
            ReplaceBuilder.Build(context);
            //FindParser.Parse(context, context.File.Data);
        }
    }
}