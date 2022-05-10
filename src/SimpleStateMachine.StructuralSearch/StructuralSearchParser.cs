
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

        public IEnumerable<IRule> FindRules { get; set; }

        public IReplaceBuilder ReplaceBuilder { get; set; }

        public IEnumerable<ReplaceRule> ReplaceRules { get; set; }
        
        public StructuralSearchParser(Configuration configuration)
        {
            FindParser = StructuralSearch.ParseFindTemplate(configuration.FindTemplate);
            
            FindRules = configuration.FindRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseFindRule);
            
            ReplaceBuilder = StructuralSearch.ParseReplaceTemplate(configuration.ReplaceTemplate);
            
            ReplaceRules = configuration.ReplaceRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseReplaceRule);
        }

        public IEnumerable<FindParserMatch> Parse(ref IParsingContext context)
        {
            var matches = FindParser.Parse(ref context);
            return matches;
        }

        public IEnumerable<FindParserMatch> ApplyFindRule(ref IParsingContext context, IEnumerable<FindParserMatch> matches)
        {
            var result = new List<FindParserMatch>();
            foreach (var match in matches)
            {
                context.Set(match.Placeholders);
                var all = FindRules.All(x => x.Execute());
                if (all)
                {
                    result.Add(match);   
                }
            }
            
            return result;
        }
        public void ApplyReplaceRule(ref IParsingContext context, IEnumerable<FindParserMatch> matches)
        {
            // var result = new List<FindParserMatch>();
            // foreach (var match in matches)
            // {
            //     context.Set(match.Placeholders);
            //     
            //     var rules = ReplaceRules
            //         .Where(x => x.FindRule.Execute())
            //         .SelectMany(x => x.Rules);
            //     
            //     var placeHolder = match.Placeholders
            //         .ToDictionary(x=> x.Key, x=> x.Value);
            //  
            //     foreach (var rule in rules)
            //     {
            //         placeHolder[rule.Placeholder.Name].Value 
            //     }
            // }
            //
            // return result;
        }
        public void Replace(FindParserMatch context)
        {
            //ReplaceBuilder.Build(context);
            // context.
            //FindParser.Parse(context, context.File.Data);
        }
        
    }
}