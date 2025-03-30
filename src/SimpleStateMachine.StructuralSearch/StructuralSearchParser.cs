using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Operator.Logical;
using SimpleStateMachine.StructuralSearch.Replace;

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
            .Select(Parsing.StructuralSearch.ParseFindRule).ToArray();

        _findParser = Parsing.StructuralSearch.ParseFindTemplate(configuration.FindTemplate);

        _replaceBuilder = Parsing.StructuralSearch.ParseReplaceTemplate(configuration.ReplaceTemplate);

        _replaceRules = configuration.ReplaceRules
            .EmptyIfNull()
            .Select(Parsing.StructuralSearch.ParseReplaceRule).ToList();
    }

    public List<FindParserResult> StructuralSearch(IInput input)
        => _findParser.Parse(input, _findRules);

    public List<ReplaceResult> StructuralSearchAndReplace(IInput input)
    {
        var results = StructuralSearch(input);
        return results.Select(r => Replace(input, r)).ToList();
    }

    public ReplaceResult Replace(IInput input, FindParserResult findResult)
    {
        IParsingContext context = new ParsingContext(input, []);

        foreach (var placeholder in findResult.Placeholders)
            context.Add(placeholder.Key, placeholder.Value);

        foreach (var replaceRule in _replaceRules)
        {
            if (!replaceRule.IsMatch(ref context))
                continue;

            foreach (var assignment in replaceRule.Assignments)
            {
                var placeholder = assignment.GetPlaceholder(ref context);
                var newValue = assignment.GetValue(ref context);
                context[placeholder.Name] = new PlaceholderReplace(placeholder, newValue);
            }
        }

        var newPlaceholders = context.ToDictionary();
        var result = _replaceBuilder.Build(ref context);
        return new ReplaceResult(findResult, newPlaceholders, result);
    }
}