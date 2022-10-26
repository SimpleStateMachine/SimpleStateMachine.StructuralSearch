
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class StructuralSearchParser
    {
        private readonly IFindParser _findParser;
        private readonly IReadOnlyList<IRule> _findRules;
        private readonly IReplaceBuilder _replaceBuilder;
        private readonly IReadOnlyList<IReplaceRule> _replaceRules;
        
        public StructuralSearchParser(Configuration configuration)
        {
            _findParser = StructuralSearch.ParseFindTemplate(configuration.FindTemplate);
            
            _findRules = configuration.FindRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseFindRule).ToList();
            
            _replaceBuilder = StructuralSearch.ParseReplaceTemplate(configuration.ReplaceTemplate);
            
            _replaceRules = configuration.ReplaceRules
                .EmptyIfNull()
                .Select(StructuralSearch.ParseReplaceRule).ToList();
        }

        public IEnumerable<FindParserResult> Parse(ref IParsingContext context)
        {
            var matches = _findParser.Parse(ref context);
            return matches;
        }

        public IEnumerable<FindParserResult> ApplyFindRule(ref IParsingContext context, IEnumerable<FindParserResult> matches)
        {
            var result = new List<FindParserResult>();
            
            foreach (var match in matches)
            {
                context.Fill(match.Placeholders);

                if (FindRuleIsMatch(match, ref context))
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

                List<ReplaceSubRule> rules = new();
                foreach (var replaceRule in _replaceRules)
                {
                    if (replaceRule.IsMatch(ref context))
                    {
                        rules.AddRange(replaceRule.Rules);
                    }
                }
                
                foreach (var rule in rules)
                {
                    var name = rule.Placeholder.Name;
                    var placeholder = placeholders[name];
                    var value = rule.Parameter.GetValue(ref context);
                    placeholders[name] = new ReplacedPlaceholder(placeholder, value);
                }
                
                result.Add(match with { Placeholders = placeholders });
            }
            
            return result;
        }
        public ReplaceMatch GetReplaceMatch(ref IParsingContext context, FindParserResult parserResult)
        {
            context.Fill(parserResult.Placeholders);
            var replaceText = _replaceBuilder.Build(ref context);
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

        private bool FindRuleIsMatch(FindParserResult parserResult, ref IParsingContext context)
        {
            foreach (var findRule in _findRules)
            {
                if (!findRule.Execute(ref context))
                    return false;
            }

            return true;
        }
    }
}