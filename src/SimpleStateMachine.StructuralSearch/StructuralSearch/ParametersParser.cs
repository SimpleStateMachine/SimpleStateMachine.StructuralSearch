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

    // atomic_token = placeholder | string_literal | whitespace
    private static readonly Parser<char, IParameter> AtomicToken =
        Parser.OneOf
        (
            PlaceholderParameter.Cast<IParameter>(), 
            StringLiteral.Cast<IParameter>(),
            WhiteSpaces.Cast<IParameter>()
        );

    // 'Length'
    private static readonly Parser<char, Func<PlaceholderParameter, IPlaceholderProperty>> PlaceholderLength = 
        CommonParser.Length.Select<Func<PlaceholderParameter, IPlaceholderProperty>>(_ => placeholder => new PlaceholderLength(placeholder));

    // 'Input.' identifier
    private static readonly Parser<char, Func<PlaceholderParameter, IPlaceholderProperty>> PlaceholderInput = 
        CommonParser.Input.Then(CommonParser.Dote).Then(Grammar.Identifier)
            .Select<Func<PlaceholderParameter, IPlaceholderProperty>>(property => placeholder => new PlaceholderInput(placeholder, property));

    // ('Offset' | 'Line' | 'Column') '.' ('Start' | 'End')
    private static readonly Parser<char, Func<PlaceholderParameter, IPlaceholderProperty>> PlaceholderPosition = 
        Parser.CIEnum<PlaceholderPositionType>().Before(CommonParser.Dote)
            .Then(Parser.CIEnum<PlaceholderPositionSubProperty>(), (property, subProperty) => (property, subProperty))
            .Select<Func<PlaceholderParameter, IPlaceholderProperty>>(x => placeholder => new PlaceholderPosition(placeholder, x.property, x.subProperty));

    // chainable_string = { '.' ('Trim' | 'TrimEnd' | 'TrimStart' | 'ToUpper' | 'ToLower') }
    private static readonly Parser<char, Func<IParameter, IParameter>> ChainableString =
        CommonParser.Dote.Then(Parser.CIEnum<StringUnaryOperator>())
            .Select<Func<IParameter, StringUnaryParameter>>(@operator => placeholder => new StringUnaryParameter(placeholder, @operator))
            .AtLeastOnce().Select<Func<IParameter, IParameter>>(funcList =>
            placeholder => funcList.Aggregate(placeholder, (parameter, func) => func(parameter)));

    // property_access = placeholder base_property [ chainable_string ]
    // base_property =
    // '.' 'Length'
    // | '.' 'Input.' identifier
    // | '.' ('Offset' | 'Line' | 'Column') '.' ('Start' | 'End')
    private static readonly Parser<char, IParameter> PropertyAccess =
        PlaceholderParameter.Before(CommonParser.Dote)
            .Then(Parser.OneOf(PlaceholderLength, PlaceholderInput, PlaceholderPosition), (placeholder, buildPropertyFunc) => buildPropertyFunc(placeholder))
            .Then(ChainableString.Optional(), (placeholderProperty, chainableStringFunc) => chainableStringFunc.HasValue ? chainableStringFunc.Value(placeholderProperty) : placeholderProperty);

    internal static readonly Parser<char, IParameter> StringExpression =
        Parser.OneOf(Parsers.BetweenParentheses
            (
                expr: Parser.Rec(() => StringExpression ?? throw new ArgumentNullException(nameof(StringExpression))),
                mapFunc: IParameter (c1, value, c2) => new ParenthesisedParameter(GetParenthesisType((c1, c2)), value)
            ), PropertyAccess, AtomicToken)
            .AtLeastOnce()
            .Select<IParameter>(parameters => new StringJoinParameter(parameters.ToList()));

    private static ParenthesisType GetParenthesisType((char c1, char c2) parenthesis)
        => parenthesis switch
        {
            (Constant.LeftParenthesis, Constant.RightParenthesis) => ParenthesisType.Usual,
            (Constant.LeftSquareParenthesis, Constant.RightSquareParenthesis) => ParenthesisType.Square,
            (Constant.LeftCurlyParenthesis, Constant.RightCurlyParenthesis) => ParenthesisType.Curly,
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesis), parenthesis, null)
        };
}