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
                Parsers.Enum<ChangeType>(true))
                .As<char, ChangeParameter, IRuleParameter>()
                .Try()
                .TrimStart();
        
        internal static readonly Parser<char, ReplaceRule> ReplaceRule =
            Parser.Map((rule, parameter) => new ReplaceRule(rule, parameter),
                    FindRuleParser.Rule.Before(CommonTemplateParser.Should),
                    Parser.OneOf(ChangeParameter, ParametersParser.Parameter));
        
        internal static ReplaceRule ParseTemplate(string str)
        {
            return ReplaceRule.ParseOrThrow(str);
        }
    }
}