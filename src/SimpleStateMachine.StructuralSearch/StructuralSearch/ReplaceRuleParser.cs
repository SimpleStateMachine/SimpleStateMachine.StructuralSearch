using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class ReplaceRuleParser
    {
        internal static readonly Parser<char, string> Then =
            Parser.CIString(Constant.Then)
                .Try()
                .TrimStart();

        internal static readonly Parser<char, IRuleParameter> ReplaceSubRuleParameter =
            Parser.Map((parameter, changeType) =>
                        changeType.HasValue ? new ChangeParameter(parameter, changeType.Value) : parameter,
                    ParametersParser.Parameter,
                    CommonParser.Dote.Then(Parser.CIEnum<ChangeType>()).Optional())
                .Try()
                .TrimStart();

        internal static readonly Parser<char, ReplaceSubRule> ReplaceSubRule =
            Parser.Map((placeholder, _, parameter) => new ReplaceSubRule(placeholder, parameter),
                    ParametersParser.PlaceholderParameter.TrimStart(),
                    CommonTemplateParser.Should.TrimStart(),
                    ReplaceSubRuleParameter)
                .Try()
                .TrimStart();

        internal static readonly Parser<char, IRule> EmptySubRule =
            CommonParser.Underscore.ThenReturn(new EmptySubRule())
                .As<char, EmptySubRule, IRule>()
                .Try()
                .TrimStart();
        
        internal static readonly Parser<char, ReplaceRule> ReplaceRule =
            Parser.Map((rule, subRules) => new ReplaceRule(rule, subRules),
                    Parser.OneOf(EmptySubRule, FindRuleParser.Expr),
                    Then.Then(ReplaceSubRule.SeparatedAtLeastOnce(CommonParser.Comma)))
                .Try()
                .TrimStart();

        internal static ReplaceRule ParseTemplate(string str)
        {
            return ReplaceRule.Before(CommonParser.EOF).ParseOrThrow(str);
        }
    }
}