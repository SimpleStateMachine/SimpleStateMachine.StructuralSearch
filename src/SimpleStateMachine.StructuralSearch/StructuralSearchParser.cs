using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public class StructuralSearchParser
{
    private readonly IFindParser _findParser;
    private readonly IReadOnlyList<IFindRule> _findRules;
    private readonly IReplaceBuilder _replaceBuilder;
    private readonly IReadOnlyList<IReplaceRule> _replaceRules;

    public StructuralSearchParser(Configuration configuration)
    {
        _findRules = configuration.FindRules
            .EmptyIfNull()
            .Select(StructuralSearch.ParseFindRule).ToList();
            
        _findParser = StructuralSearch.ParseFindTemplate(configuration.FindTemplate, _findRules);
            
        _replaceBuilder = StructuralSearch.ParseReplaceTemplate(configuration.ReplaceTemplate);
            
        _replaceRules = configuration.ReplaceRules
            .EmptyIfNull()
            .Select(StructuralSearch.ParseReplaceRule).ToList();
    }

    public IEnumerable<FindParserResult> Parse(IInput input)
        => _findParser.Parse(input);

    // public IEnumerable<FindParserResult> ApplyReplaceRule(IEnumerable<FindParserResult> matches)
    // {
    //     var result = new List<FindParserResult>();
    //         
    //     foreach (var match in matches)
    //     {
    //         var placeholders = match.Placeholders
    //             .ToDictionary(x => x.Key, 
    //                 x => x.Value);
    //
    //         IParsingContext context = new ParsingContext(match.Placeholders.First().Value.Input, match.Placeholders);
    //         List<ReplaceSubRule> rules = new();
    //
    //         foreach (var replaceRule in _replaceRules)
    //         {
    //             if (replaceRule.IsMatch(ref context))
    //             {
    //                 rules.AddRange(replaceRule.Rules);
    //             }
    //         }
    //
    //         foreach (var rule in rules)
    //         {
    //             var name = rule.Placeholder.Name;
    //             var placeholder = placeholders[name];
    //             var value = rule.Parameter.GetValue(ref context);
    //             placeholders[name] = new ReplacedPlaceholder(placeholder, value);
    //         }
    //
    //         result.Add(match with { Placeholders = placeholders });
    //     }
    //         
    //     return result;
    // }

    // public ReplaceMatch GetReplaceMatch(FindParserResult parserResult)
    // {
    //     IParsingContext context = new ParsingContext(parserResult.Placeholders);
    //     var replaceText = _replaceBuilder.Build(ref context);
    //     return new ReplaceMatch(parserResult.Match, replaceText);
    // }
    //
    // public IEnumerable<ReplaceMatch> GetReplaceMatches(IEnumerable<FindParserResult> parserResults)
    //     => parserResults.Select(GetReplaceMatch);
}