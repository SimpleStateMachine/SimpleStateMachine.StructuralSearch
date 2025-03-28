// using Pidgin;
// using SimpleStateMachine.StructuralSearch.Extensions;
// using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
// using SimpleStateMachine.StructuralSearch.Parameters;
// using SimpleStateMachine.StructuralSearch.Rules.FindRules;
//
// namespace SimpleStateMachine.StructuralSearch.StructuralSearch;
//
// internal static class FindSubRuleParser
// {
//     private static readonly Parser<char, StringCompareType2> SubRuleType = Parser.CIEnum<StringCompareType2>().Trim();
//     private static readonly Parser<char, ParameterType> PlaceholderType = Parser.CIEnum<ParameterType>().TrimStart();
//
//     private static Parser<char, IFindRule> BinarySubRule(IParameter left, StringCompareType2 ruleType) =>
//         ParametersParser.Parameter
//             .TrimStart()
//             .Select<IFindRule>(right => new BinarySubRule(ruleType, left, right))
//             .Try();
//
//     private static Parser<char, IFindRule> IsSubRule(IParameter left) =>
//         PlaceholderType.Select<IFindRule>(arg => new IsSubRule(left, arg))
//             .Try();
//
//     private static Parser<char, IFindRule> InSubRule(IParameter left) =>
//         ParametersParser.Parameters
//             .ParenthesisedOptional(x => Parser.Char(x).Trim())
//             .TrimStart()
//             .Select<IFindRule>(args => new InSubRule(left, args))
//             .Try();
//
//     internal static readonly Parser<char, IFindRule> OneOfSubRule =
//         Parser.Map
//             (
//                 func: (left, ruleType) => (value: left, type: ruleType),
//                 parser1: Parser.OneOf
//                 (
//                     PlaceholderPropertyParser.PlaceholderPropertyParameter.Cast<IParameter>().Try(), 
//                     ParametersParser.PlaceholderParameter.Cast<IParameter>()
//                 ).Trim(),
//                 parser2: SubRuleType
//             )
//             .Then(x => x.type switch
//             {
//                 Rules.FindRules.Types.StringCompareType2.In => InSubRule(x.value),
//                 Rules.FindRules.Types.StringCompareType2.Is => IsSubRule(x.value),
//                 _ => BinarySubRule(x.value, x.type)
//             });
// }