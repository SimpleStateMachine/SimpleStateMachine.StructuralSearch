using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

public class ReplaceTemplateParser
{
    private static readonly Parser<char, IParameter> StringLiteral =
        Grammar.TemplateStringLiteral.AtLeastOnceString()
            .Select<IParameter>(str => new StringParameter(str));

    private static readonly Parser<char, IParameter> WhiteSpaces =
        Grammar.WhiteSpaces.Select<IParameter>(str => new StringParameter(str));

    // template_component = placeholder | template_string_literal | whitespace
    private static readonly Parser<char, IParameter> TemplateComponent
        = Parser.OneOf(ParametersParser.PlaceholderParameter.Cast<IParameter>(), StringLiteral, WhiteSpaces)
            .AtLeastOnce().Select(ParametersParser.JoinParameters);

    // template_between_parentheses = '(' template ')' | '{' template '}' | '[' template ']
    private static readonly Parser<char, IParameter> TemplateBetweenParentheses
        = Parser.Rec(() => ReplaceTemplate ?? throw new ArgumentNullException(nameof(ReplaceTemplate))).Optional()
            .BetweenParentheses(IParameter (c1, optionalValue, c2) =>
            {
                var value = optionalValue.GetValueOrDefault(StringParameter.Empty);
                return new ParenthesisedParameter(Grammar.GetParenthesisType((c1, c2)), value);
            });

    // template = (template_component | template_between_parentheses)+
    internal static readonly Parser<char, IParameter> ReplaceTemplate =
        Parser.OneOf(TemplateBetweenParentheses, TemplateComponent)
            .AtLeastOnce().Select(ParametersParser.JoinParameters);
}