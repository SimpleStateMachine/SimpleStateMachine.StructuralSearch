using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate
{
    public static class ParserToReplace
    {
        public static Parser<char, IReplaceStep> ResultAsReplace(Parser<char, string> parser)
        {
            return parser.Select(result => new TokenReplace(result))
                .As<char, TokenReplace, IReplaceStep>();
        }
        
        public static Parser<char, IReplaceStep> Stringc(char token, bool ignoreCase = false)
        {
            var value = token.ToString();
            
            return ResultAsReplace(Parsers.Parsers.String(value, ignoreCase));
        }
    }
}