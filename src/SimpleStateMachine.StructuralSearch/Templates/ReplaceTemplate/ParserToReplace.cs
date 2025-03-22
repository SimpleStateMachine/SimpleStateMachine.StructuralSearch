namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

// internal static class ParserToReplace
// {
//     public static Parser<char, IReplaceStep> ResultAsReplace(Parser<char, string> parser)
//         => parser.Select(result => new TokenReplace(result))
//             .As<char, TokenReplace, IReplaceStep>();
//
//     public static Parser<char, IReplaceStep> Stringc(char token, bool ignoreCase = false)
//     {
//         var tokenStr = token.ToString();
//         return ResultAsReplace(Parsers.Parsers.String(tokenStr, ignoreCase));
//     }
// }