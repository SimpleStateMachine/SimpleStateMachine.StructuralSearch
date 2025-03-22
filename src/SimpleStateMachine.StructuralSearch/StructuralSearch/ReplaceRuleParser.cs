using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

internal static class ReplaceRuleParser
{
    private static readonly Parser<char, string> Then =
        Parser.CIString(Constant.Then)
            .Try()
            .TrimStart();

    private static readonly Parser<char, ReplaceSubRule> ReplaceSubRule =
        Parser.Map((placeholder, _, parameter) => new ReplaceSubRule(placeholder, parameter),
                ParametersParser.PlaceholderParameter.TrimStart(),
                CommonTemplateParser.Should.TrimStart(),
                ParametersParser.Parameter.TrimStart())
            .Try()
            .TrimStart();

    private static readonly Parser<char, IFindRule> EmptySubRule =
        CommonParser.Underscore.ThenReturn(new EmptySubRule())
            .As<char, EmptySubRule, IFindRule>()
            .Try()
            .TrimStart();

    private static readonly Parser<char, ReplaceRule> ReplaceRule =
        Parser.Map((rule, subRules) => new ReplaceRule(rule, subRules),
                Parser.OneOf(EmptySubRule, FindRuleParser.Expr),
                Then.Then(ReplaceSubRule.SeparatedAtLeastOnce(CommonParser.Comma)))
            .Try()
            .TrimStart();

    internal static IReplaceRule ParseTemplate(string? str) 
        => string.IsNullOrEmpty(str)
            ? SimpleStateMachine.StructuralSearch.ReplaceRule.Empty
            : ReplaceRule.Before(CommonParser.EOF).ParseOrThrow(str);
}