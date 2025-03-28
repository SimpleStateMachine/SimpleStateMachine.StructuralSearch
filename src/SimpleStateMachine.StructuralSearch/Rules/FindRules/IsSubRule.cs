// using System;
// using SimpleStateMachine.StructuralSearch.Context;
// using SimpleStateMachine.StructuralSearch.Extensions;
// using SimpleStateMachine.StructuralSearch.Helper;
// using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
// using SimpleStateMachine.StructuralSearch.Parameters;
// using SimpleStateMachine.StructuralSearch.StructuralSearch;
//
// namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;
//
// internal class IsSubRule : IFindRule
// {
//     private readonly ParameterType _argument;
//     private readonly IParameter _parameter;
//         
//     public IsSubRule(IParameter parameter, ParameterType argument)
//     {
//         _parameter = parameter;
//         _argument = argument;
//     }
//
//     public bool IsApplicableForPlaceholder(string placeholderName)
//         => _parameter.IsApplicableForPlaceholder(placeholderName);
//
//     public bool Execute(ref IParsingContext context)
//     {
//         var value = _parameter.GetValue(ref context);
//             
//         return _argument switch
//         {
//             ParameterType.Var => Grammar.Identifier.Before(CommonParser.Eof).TryParse(value, out _),
//             ParameterType.Int => int.TryParse(value, out _),
//             ParameterType.Double => double.TryParse(value, out _),
//             ParameterType.DateTime => DateTime.TryParse(value, out _),
//             ParameterType.Guid => Guid.TryParse(value, out _),
//             _ => throw new ArgumentOutOfRangeException(nameof(_argument).FormatPrivateVar(), _argument, null)
//         };
//     }
//         
//     public override string ToString() 
//         => $"{_parameter}{Constant.Space}{StringCompareType2.Is}{Constant.Space}{_argument}";
// }