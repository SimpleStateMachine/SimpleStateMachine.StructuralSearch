using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public static class StructuralSearch
    {
        public static Parser<char, SourceMatch> ParseTemplate(string str) 
            => FindTemplateParser.ParseTemplate(str);
    }
}