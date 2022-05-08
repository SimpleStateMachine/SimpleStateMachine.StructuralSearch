
using System.Collections.Generic;
using System.IO;
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

        public Dictionary<string, PlaceholderLogicalRule> FindRules { get; set; }

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

        public IEnumerable<FindParserMatch> Parse(ref IParsingContext context)
        {
            var matches = FindParser.Parse(ref context);
            var result = new List<FindParserMatch>();
            
            foreach (var match in matches)
            {
               context.Set(match.Placeholders);
               if (AllRulesCompleted(ref context))
               {
                    result.Add(match);   
               }
            }
            
            return result;
        }
        
        public void Replace(FindParserMatch context)
        {
            //ReplaceBuilder.Build(context);
            // context.
            //FindParser.Parse(context, context.File.Data);
        }
        
        public bool AllRulesCompleted(ref IParsingContext context)
        {
            return FindRules.Values.All(x => x.Execute());
        }
    }
}