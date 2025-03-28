// using System;
// using System.Linq;
// using Pidgin;
// using SimpleStateMachine.StructuralSearch.Operator.String.Type;
// using SimpleStateMachine.StructuralSearch.Parameters;
// using SimpleStateMachine.StructuralSearch.Parameters.Types;
//
// namespace SimpleStateMachine.StructuralSearch.StructuralSearch;
//
// internal static class ChangeParameterParser
// {
//     
//     private delegate IStringParameter StringRuleParameterFactory(IStringParameter stringParameter);
//
//     private static readonly Parser<char, StringRuleParameterFactory> ChangeUnaryParameter =
//         Parser.CIEnum<StringUnaryOperator>()
//             .Select<StringRuleParameterFactory>(changeType =>
//                 placeholder => new ChangeUnaryParameter(placeholder, changeType));
//
//     private static readonly Parser<char, StringRuleParameterFactory> ChangeBinaryParameter = Parser.Map
//         (
//             func: (type, arg) => (type, arg),
//             parser1: Parser.CIEnum<ChangeBinaryType>(),
//             parser2: CommonParser.Parenthesised(Parser.Rec(() => ChangeStringParameter ?? throw new ArgumentNullException()), Parser.Char)
//         )
//         .Select<StringRuleParameterFactory>(pair => placeholder => new ChangeBinaryParameter(placeholder, pair.type, right: pair.arg));
//
//     private static readonly Parser<char, StringRuleParameterFactory> ChangeParameter =
//         CommonParser.Dote.Then(Parser.OneOf(ChangeUnaryParameter, ChangeBinaryParameter))
//             .Many()
//             .Select<StringRuleParameterFactory>(funcList =>
//                 placeholder => funcList.Aggregate(placeholder, (parameter, func) => func(parameter)));
//
//     internal static readonly Parser<char, IStringParameter> ChangeStringParameter =
//         Parser.OneOf(Parser.Rec(() => ParametersParser.PlaceholderParameter.Cast<IStringParameter>()), StringParameterParser.StringParameter)
//             .Then(ChangeParameter, (parameter, factory) => factory(parameter));
//     
// }