
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

        public IEnumerable<FindParserResult> Parse(ref IParsingContext context)
        {
            var matches = FindParser.Parse(ref context);
            return matches;
        }

        public IEnumerable<FindParserResult> ApplyFindRule(ref IParsingContext context, IEnumerable<FindParserResult> matches)
        {
            var result = new List<FindParserResult>();
            foreach (var match in matches)
            {
                context.Fill(match.Placeholders);
                var all = FindRules.All(x => x.Execute());
                if (all)
                {
                    result.Add(match);   
                }
            }
            
            return result;
        }
        public IEnumerable<FindParserResult> ApplyReplaceRule(ref IParsingContext context, IEnumerable<FindParserResult> matches)
        {
            var result = new List<FindParserResult>();
            
            foreach (var match in matches)
            {
                var placeholders = match.Placeholders
                    .ToDictionary(x => x.Key, 
                    x => x.Value);
                
                context.Fill(match.Placeholders);
                
                var rules = ReplaceRules
                    .Where(x => x.ConditionRule.Execute())
                    .SelectMany(x => x.Rules);

                foreach (var rule in rules)
                {
                    var name = rule.Placeholder.Name;
                    var placeholder = placeholders[name];
                    var value = rule.Parameter.GetValue();
                    placeholders[name] = new ReplacedPlaceholder(placeholder, value);
                }
                
                result.Add(match with { Placeholders = placeholders });
            }
            
            return result;
        }
        public void Replace(ref IParsingContext context, FindParserResult parserResult)
        {
            context.Fill(parserResult.Placeholders);
            var replaceText = ReplaceBuilder.Build(context);
            context.Input.ReplaceAsync(parserResult.Match, replaceText);
            //ReplaceBuilder.Build(context);
            // context.
            //FindParser.Parse(context, context.File.Data);
        }
        public void Replace(ref IParsingContext context, IEnumerable<FindParserResult> parserResults)
        {
            foreach (var parserResult in parserResults)
            {
                Replace(ref context, parserResult);
            }
            
            //ReplaceBuilder.Build(context);
            // context.
            //FindParser.Parse(context, context.File.Data);
        }
    }
}