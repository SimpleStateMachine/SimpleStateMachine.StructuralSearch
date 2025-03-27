using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ReplaceRuleParser
{
    private static readonly Parser<char, string> Then =
        Parser.CIString(Constant.Then).Try().TrimStart();

    private static readonly Parser<char, string> If =
        Parser.CIString(Constant.If).Try().TrimStart();

    internal static readonly Parser<char, ReplaceCondition> ReplaceCondition =
        If.Then(FindRuleParser.Expr).Before(Then)
            .Select(findRule => new ReplaceCondition(findRule));

    private static readonly Parser<char, ReplaceSubRule> ReplaceSubRule =
        Parser.Map
        (
            func: (placeholder, _, parameter) => new ReplaceSubRule(placeholder, parameter),
            parser1: ParametersParser.PlaceholderParameter.TrimStart(),
            parser2: CommonParser.Should.TrimStart(),
            parser3: ParametersParser.Parameter.TrimStart()
        ).Try().TrimStart();

    private static readonly Parser<char, ReplaceRule> ReplaceRule =
        Parser.Map
        (
            func: (rule, subRules) => new ReplaceRule(rule, subRules),
            parser1: ReplaceCondition.Optional().Select<IFindRule>(r => r.HasValue ? r.Value : EmptyFindRule.Instance),
            parser2: ReplaceSubRule.SeparatedAtLeastOnce(CommonParser.Comma)
        ).Try().TrimStart();

    internal static IReplaceRule ParseTemplate(string? str)
        => string.IsNullOrEmpty(str)
            ? Rules.ReplaceRules.ReplaceRule.Empty
            : ReplaceRule.Before(CommonParser.Eof).ParseOrThrow(str);
}