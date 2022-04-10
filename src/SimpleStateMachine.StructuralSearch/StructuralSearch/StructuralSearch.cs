using SimpleStateMachine.StructuralSearch.Rules.FindRule;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRule;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch
{
    public static class StructuralSearch
    {
        public static IFindParser ParseFindTemplate(string template) 
            => FindTemplateParser.ParseTemplate(template);
        
        public static IReplaceBuilder ParseReplaceTemplate(string template) 
            => ReplaceTemplateParser.ParseTemplate(template);
        
        public static FindRule ParseFindRule(string template) 
            => FindRuleParser.ParseTemplate(template);
        
        public static ReplaceRule ParseReplaceRule(string template) 
            => ReplaceRuleParser.ParseTemplate(template);
    }
}