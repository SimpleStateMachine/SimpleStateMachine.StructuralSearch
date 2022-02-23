using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class ParserToParser
    {
        public static Parser<char, Parser<char, string>> String(string token)
        {
            return Parser.String(token).Select(x => Parser.String(token));
        }
        
        public static Parser<char, Parser<char, string>> Stringc(char token)
        {
            var _token = token.ToString();
            return Parser.String(_token).Select(x => Parser.String(_token));
        }
    }
}