using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.String;
using SimpleStateMachine.StructuralSearch.Operator.String.Type;
using SimpleStateMachine.StructuralSearch.Parameters;
using SimpleStateMachine.StructuralSearch.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ParametersParser
{
    // placeholder = '$' identifier '$'
    internal static readonly Parser<char, PlaceholderParameter> PlaceholderParameter =
        Grammar.Placeholder.Select(x => new PlaceholderParameter(x)).Try();
    
    // string_literal = <escaped string>
    internal static readonly Parser<char, StringParameter> StringLiteral =
        Grammar.StringLiteral.Select(x => new StringParameter(x));
    
    // whitespace = (' ' | '\n' | '\r')+
    internal static readonly Parser<char, StringParameter> WhiteSpaces =
        Grammar.WhiteSpaces.Select(x => new StringParameter(x));

    // atomic_token = placeholder | string_literal
    private static readonly Parser<char, IParameter> AtomicToken =
        Parser.OneOf
        (
            PlaceholderParameter.Cast<IParameter>(), 
            StringLiteral.Cast<IParameter>()
        );

    // 'Length'
    internal static readonly Parser<char, Func<PlaceholderParameter, IPlaceholderProperty>> PlaceholderLength = 
        CommonParser.Length.Select<Func<PlaceholderParameter, IPlaceholderProperty>>(_ => placeholder => new PlaceholderLength(placeholder));

    // 'Input.' identifier
    internal static readonly Parser<char, Func<PlaceholderParameter, IPlaceholderProperty>> PlaceholderInput = 
        CommonParser.Input.Then(CommonParser.Dote).Then(Grammar.Identifier)
            .Select<Func<PlaceholderParameter, IPlaceholderProperty>>(property => placeholder => new PlaceholderInput(placeholder, property));

    // ('Offset' | 'Line' | 'Column') '.' ('Start' | 'End')
    internal static readonly Parser<char, Func<PlaceholderParameter, IPlaceholderProperty>> PlaceholderPosition = 
        Parser.CIEnum<PlaceholderPositionType>().Before(CommonParser.Dote)
            .Then(Parser.CIEnum<PlaceholderPositionSubProperty>(), (property, subProperty) => (property, subProperty))
            .Select<Func<PlaceholderParameter, IPlaceholderProperty>>(x => placeholder => new PlaceholderPosition(placeholder, x.property, x.subProperty));

    // chainable_string = { '.' ('Trim' | 'TrimEnd' | 'TrimStart' | 'ToUpper' | 'ToLower') }
    internal static readonly Parser<char, Func<IParameter, IParameter>> ChainableString =
        Parser.CIEnum<StringUnaryOperator>().After(CommonParser.Dote).AtLeastOnce()
            .Select<Func<IParameter, IParameter>>(operators =>
                parameter => operators.Aggregate(parameter, (param, op) => new StringUnaryParameter(param, op)));

    // property_access = placeholder { '.' ( placeholder_length | placeholder_input | placeholder_position ) } [ chainable_string ]
    // placeholder_length = 'Length'
    // placeholder_input = 'Input.' identifier
    // placeholder_position = ('Offset' | 'Line' | 'Column') '.' ('Start' | 'End')
    internal static readonly Parser<char, IParameter> PropertyAccess =
        PlaceholderParameter
            .Then(CommonParser.Dote.Then(Parser.OneOf(PlaceholderLength.Try(), PlaceholderInput.Try(), PlaceholderPosition)).Try().Optional(), 
                (placeholder, buildPropertyFunc) => buildPropertyFunc.HasValue ? buildPropertyFunc.Value(placeholder) : placeholder)
            .Then(ChainableString.Optional(), (placeholderProperty, chainableStringFunc) => chainableStringFunc.HasValue ? chainableStringFunc.Value(placeholderProperty) : placeholderProperty);
    
    // string_expr =
    // ( '(' string_expr ')'
    // | '{' string_expr '}'
    // | '[' string_expr ']'
    // | property_access
    // | atomic_token
    // )+
    internal static readonly Parser<char, IParameter> StringExpression =
        Parser.OneOf(Parsers.BetweenParentheses
            (
                expr: Parser.Rec(() => StringExpression ?? throw new ArgumentNullException(nameof(StringExpression))),
                mapFunc: IParameter (c1, value, c2) => new ParenthesisedParameter(GetParenthesisType((c1, c2)), value)
            ), PropertyAccess, AtomicToken)
            .AtLeastOnce()
            .Select<IParameter>(parameters =>
            {
                var parametersList = parameters.ToList();
                return parametersList.Count == 1 ? parametersList[0] : new StringJoinParameter(parametersList);
            });

    private static ParenthesisType GetParenthesisType((char c1, char c2) parenthesis)
        => parenthesis switch
        {
            (Constant.LeftParenthesis, Constant.RightParenthesis) => ParenthesisType.Usual,
            (Constant.LeftSquareParenthesis, Constant.RightSquareParenthesis) => ParenthesisType.Square,
            (Constant.LeftCurlyParenthesis, Constant.RightCurlyParenthesis) => ParenthesisType.Curly,
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesis), parenthesis, null)
        };
}