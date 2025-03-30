using System;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Operator.Logical;
using SimpleStateMachine.StructuralSearch.Parameters;
using SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class StructuralSearch
{
    public static IFindParser ParseFindTemplate(string? template)
    {
        var parsers = string.IsNullOrEmpty(template) 
            ? [] 
            : FindTemplateParser.Template.ParseOrThrow(template).ToList();

        var templateParser = new CustomParsers.FindTemplateParser(parsers);
        return new FindParser(templateParser);
    }

    public static IReplaceBuilder ParseReplaceTemplate(string? template)
    {
        var parameter = string.IsNullOrEmpty(template)
            ? StringParameter.Empty
            : ReplaceTemplateParser.ReplaceTemplate.ParseOrThrow(template);

        return new ReplaceBuilder(parameter);
    }

    public static ILogicalOperation ParseFindRule(string? template)
        => string.IsNullOrEmpty(template)
            ? new EmptyLogicalOperation()
            : LogicalExpressionParser.LogicalExpression.ParseOrThrow(template);

    internal static IReplaceRule ParseReplaceRule(string? template)
        => string.IsNullOrEmpty(template)
            ? ReplaceRule.Empty
            : ReplaceRuleParser.ReplaceRule.ParseOrThrow(template);
}