using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class ReplaceRuleParser
    {
        internal static readonly Parser<char, IRuleParameter> ChangeParameter =
            Parser.Map((parameter, changeType) => new ChangeParameter(parameter, changeType),
                ParametersParser.Parameter.Before(CommonParser.Dote),
                Parser.CIEnum<ChangeType>())
                .As<char, ChangeParameter, IRuleParameter>()
                .Try()
                .TrimStart();
    
        internal static readonly Parser<char, string> Then =
            Parser.CIString(Constant.Then)
                .Try()
                .TrimStart();
        
        internal static readonly Parser<char, ReplaceSubRule> ReplaceSubRule =
            Parser.Map((placeholder, parameter) => new ReplaceSubRule(placeholder, parameter),
                    ParametersParser.PlaceholderParameter.Before(CommonTemplateParser.Should.TrimStart()),
                    Parser.OneOf(ChangeParameter.Try(), ParametersParser.Parameter.Try()))
                .Try()
                .TrimStart();
        
        internal static readonly Parser<char, ReplaceRule> ReplaceRule =
            Parser.Map((rule, subRules) => new ReplaceRule(rule, subRules),
                    FindRuleParser.Expr,
                    Then.Then(ReplaceSubRule.SeparatedAtLeastOnce(CommonParser.Comma)))
                .Try()
                .TrimStart();
        
        internal static ReplaceRule ParseTemplate(string str)
        {
            return ReplaceRule.ParseOrThrow(str);
        }
    }
}