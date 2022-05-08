using Pidgin;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class StructuralSearch
    {
        public static IFindParser ParseFindTemplate(string template) 
            => FindTemplateParser.ParseTemplate(template);
        
        public static IReplaceBuilder ParseReplaceTemplate(string template) 
            => ReplaceTemplateParser.ParseTemplate(template);
        
        public static PlaceholderLogicalRule ParseFindRule(string template) 
            => RuleParser.ParseTemplate(template);
        
        public static ReplaceRule ParseReplaceRule(string template) 
            => ReplaceRuleParser.ParseTemplate(template);
    }
}