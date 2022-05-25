
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

        public IReadOnlyList<IRule> FindRules { get; set; }

        public IReplaceBuilder ReplaceBuilder { get; set; }

        public IReadOnlyList<ReplaceRule> ReplaceRules { get; set; }
        
        public StructuralSearchParser(Configuration configuration)
        {
            FindParser = StructuralSearch.ParseFindTemplate(configuration.FindTemplate);
            
            FindRules = configuration.FindRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseFindRule).ToList();
            
            ReplaceBuilder = StructuralSearch.ParseReplaceTemplate(configuration.ReplaceTemplate);
            
            ReplaceRules = configuration.ReplaceRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseReplaceRule).ToList();
        }

        public IEnumerable<FindParserResult> Parse(ref IParsingContext context)
        {
            var matches = FindParser.Parse(ref context);
            return matches;
        }

        public IEnumerable<FindParserResult> ApplyFindRule(ref IParsingContext context, IEnumerable<FindParserResult> matches)
        {
            var result = new List<FindParserResult>();
            SetFindRulesContext(ref context);
            foreach (var match in matches)
            {
                context.Fill(match.Placeholders);
                var all = FindRules.All(x => x.Execute());
                if (all)
                {
                    result.Add(match);   
                }
            }
            
            SetFindRulesContext(ref ParsingContext.Empty);
            return result;
        }
        public IEnumerable<FindParserResult> ApplyReplaceRule(ref IParsingContext context, IEnumerable<FindParserResult> matches)
        {
            SetReplaceRulesContext(ref context);
            var result = new List<FindParserResult>();
            
            foreach (var match in matches)
            {
                var placeholders = match.Placeholders
                    .ToDictionary(x => x.Key, 
                    x => x.Value);
                
                context.Fill(match.Placeholders);
                
                var rules = ReplaceRules
                    .Where(x =>
                    {
                        var result = x.ConditionRule.Execute();
                        return result;
                    })
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
            
            SetReplaceRulesContext(ref ParsingContext.Empty);

            return result;
        }
        public ReplaceMatch GetReplaceMatch(ref IParsingContext context, FindParserResult parserResult)
        {
            context.Fill(parserResult.Placeholders);
            var replaceText = ReplaceBuilder.Build(context);
            return new ReplaceMatch(parserResult.Match, replaceText);
            // context.Input.Replace(parserResult.Match, replaceText);
            //ReplaceBuilder.Build(context);
            // context.
            //FindParser.Parse(context, context.File.Data);
        }
        public IEnumerable<ReplaceMatch> GetReplaceMatches(ref IParsingContext context, IEnumerable<FindParserResult> parserResults)
        {
            var replaceMatches = new List<ReplaceMatch>();
            foreach (var parserResult in parserResults)
            {
                var replaceMatch  = GetReplaceMatch(ref context, parserResult);
                replaceMatches.Add(replaceMatch);
            }

            return replaceMatches;
        }


        private void SetFindRulesContext(ref IParsingContext context)
        {
            foreach (var findRule in FindRules)
            {
                if(findRule is IContextDependent contextDependent)
                    contextDependent.SetContext(ref context);
            }
        }
        
        private void SetReplaceRulesContext(ref IParsingContext context)
        {
            foreach (var replaceRule in ReplaceRules)
            {
                if(replaceRule is IContextDependent contextDependent)
                    contextDependent.SetContext(ref context);
            }
        }
    }
}