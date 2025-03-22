using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class StructuralSearch
{
    public static IFindParser ParseFindTemplate(string? template) 
        => FindTemplateParser.ParseTemplate(template, System.Array.Empty<IFindRule>());
        
    public static IFindParser ParseFindTemplate(string? template, IReadOnlyList<IFindRule> findRules) 
        => FindTemplateParser.ParseTemplate(template, findRules);
        
    public static IReplaceBuilder ParseReplaceTemplate(string? template) 
        => ReplaceTemplateParser.ParseTemplate(template);
        
    public static IFindRule ParseFindRule(string? template) 
        => FindRuleParser.ParseTemplate(template);
        
    internal static IReplaceRule ParseReplaceRule(string? template) 
        => ReplaceRuleParser.ParseTemplate(template);
}