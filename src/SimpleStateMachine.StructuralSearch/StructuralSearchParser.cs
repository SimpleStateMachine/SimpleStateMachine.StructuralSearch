using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Operator.Logical;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch;

public class StructuralSearchParser
{
    private readonly IFindParser _findParser;
    private readonly ILogicalOperation[] _findRules;
    private readonly IReplaceBuilder _replaceBuilder;
    private readonly IReadOnlyList<IReplaceRule> _replaceRules;

    public StructuralSearchParser(Configuration configuration)
    {
        _findRules = configuration.FindRules
            .EmptyIfNull()
            .Select(StructuralSearch.StructuralSearch.ParseFindRule).ToArray();
            
        _findParser = StructuralSearch.StructuralSearch.ParseFindTemplate(configuration.FindTemplate);
            
        _replaceBuilder = StructuralSearch.StructuralSearch.ParseReplaceTemplate(configuration.ReplaceTemplate);
            
        _replaceRules = configuration.ReplaceRules
            .EmptyIfNull()
            .Select(StructuralSearch.StructuralSearch.ParseReplaceRule).ToList();
    }

    public IEnumerable<FindParserResult> Parse(IInput input)
        => _findParser.Parse(input, _findRules);

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