using Pidgin;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class StructuralSearch
    {
        public static Parser<char, SourceMatch> ParseFindTemplate(string template, IParsingContext context) 
            => FindTemplateParser.ParseTemplate(template, context);
        
        public static IReplaceBuilder ParseReplaceTemplate(string template) 
            => ReplaceTemplateParser.ParseTemplate(template);
        
        public static FindRule ParseFindRule(string template) 
            => FindRuleParser.ParseTemplate(template);
        
        public static ReplaceRule ParseReplaceRule(string template) 
            => ReplaceRuleParser.ParseTemplate(template);
    }
}