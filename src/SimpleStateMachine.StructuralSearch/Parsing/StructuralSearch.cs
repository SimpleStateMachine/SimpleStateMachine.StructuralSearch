using System.Linq;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.Logical;
using SimpleStateMachine.StructuralSearch.Parameters;
using SimpleStateMachine.StructuralSearch.Parsers;
using SimpleStateMachine.StructuralSearch.Replace;

namespace SimpleStateMachine.StructuralSearch.Parsing;

internal static class StructuralSearch
{
    public static IFindParser ParseFindTemplate(string? template)
    {
        var parsers = string.IsNullOrEmpty(template) 
            ? [] 
            : FindTemplateParser.Template.ParseToEnd(template).ToList();

        var templateParser = new Parsers.FindTemplate(parsers);
        return new FindParser(templateParser);
    }

    public static IReplaceBuilder ParseReplaceTemplate(string? template)
    {
        var parameter = string.IsNullOrEmpty(template)
            ? StringParameter.Empty
            : ReplaceTemplateParser.ReplaceTemplate.ParseToEnd(template);

        return new ReplaceBuilder(parameter);
    }

    public static ILogicalOperation ParseFindRule(string? template)
        => string.IsNullOrEmpty(template)
            ? new EmptyLogicalOperation()
            : LogicalExpressionParser.LogicalExpression.ParseToEnd(template);

    internal static IReplaceRule ParseReplaceRule(string? template)
        => string.IsNullOrEmpty(template)
            ? ReplaceRule.Empty
            : ReplaceRuleParser.ReplaceRule.ParseToEnd(template);
}