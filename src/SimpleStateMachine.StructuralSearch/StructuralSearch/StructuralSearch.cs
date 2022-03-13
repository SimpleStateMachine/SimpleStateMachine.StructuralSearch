using Pidgin;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class StructuralSearch
    {
        public static Parser<char, SourceMatch> ParseFindTemplate(string template) 
            => FindTemplateParser.ParseTemplate(template);
        
        public static IReplaceBuilder ParseReplaceTemplate(string template) 
            => ReplaceTemplateParser.ParseTemplate(template);
        
        public static IRule ParseFindRule(string template) 
            => FindRuleParser.ParseTemplate(template);
    }
}