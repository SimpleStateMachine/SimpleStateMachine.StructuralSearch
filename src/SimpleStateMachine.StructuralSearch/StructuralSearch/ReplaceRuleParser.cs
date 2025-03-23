using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ReplaceRuleParser
{
    private static readonly Parser<char, string> Then =
        Parser.CIString(Constant.Then).Try().TrimStart();

    private static readonly Parser<char, ReplaceSubRule> ReplaceSubRule =
        Parser.Map
        (
            func: (placeholder, _, parameter) => new ReplaceSubRule(placeholder, parameter),
            parser1: ParametersParser.PlaceholderParameter.TrimStart(),
            parser2: CommonTemplateParser.Should.TrimStart(),
            parser3: ParametersParser.Parameter.TrimStart()
        ).Try().TrimStart();

    private static readonly Parser<char, IFindRule> EmptySubRule =
        CommonParser.Underscore.ThenReturn(new EmptySubRule())
            .As<char, EmptySubRule, IFindRule>().Try().TrimStart();

    private static readonly Parser<char, ReplaceRule> ReplaceRule =
        Parser.Map
        (
            func: (rule, subRules) => new ReplaceRule(rule, subRules),
            parser1: Parser.OneOf(EmptySubRule, FindRuleParser.Expr),
            parser2: Then.Then(ReplaceSubRule.SeparatedAtLeastOnce(CommonParser.Comma))
        ).Try().TrimStart();

    internal static IReplaceRule ParseTemplate(string? str)
        => string.IsNullOrEmpty(str)
            ? Rules.ReplaceRules.ReplaceRule.Empty
            : ReplaceRule.Before(CommonParser.Eof).ParseOrThrow(str);
}