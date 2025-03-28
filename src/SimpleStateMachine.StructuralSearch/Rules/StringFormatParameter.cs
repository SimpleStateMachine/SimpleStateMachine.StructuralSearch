// using SimpleStateMachine.StructuralSearch.Context;
// using SimpleStateMachine.StructuralSearch.Parameters;
//
// namespace SimpleStateMachine.StructuralSearch.Rules;
//
// internal class StringFormatParameter : IParameter
// {
//     private readonly string _str;
//
//     public StringFormatParameter(string str)
//     {
//         _str = str;
//     }
//
//     public string GetValue(ref IParsingContext context) => _str;
//
//     public override string ToString()
//         => $"{Constant.DoubleQuotes}{_str}{Constant.DoubleQuotes}";
// }