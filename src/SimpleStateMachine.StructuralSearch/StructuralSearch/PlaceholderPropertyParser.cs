using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class PlaceholderPropertyParser
{
    private delegate IPlaceholderPropertyRuleParameter PlaceholderPropertyFactory(PlaceholderParameter parameter);

    private static readonly Parser<char, PlaceholderPropertyFactory> Input =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Input)
            .Then(CommonParser.Dote)
            .Then(Grammar.Identifier)
            .Select<PlaceholderPropertyFactory>(propertyName =>
                placeholder => new PlaceholderInputPropertyParameter(placeholder, propertyName));

    private static readonly Parser<char, PlaceholderPropertyFactory> Column =
        PlaceholderSubProperty<ColumnProperty>(PlaceholderProperty.Column, (placeholder, property)
            => new PlaceholderColumnParameter(placeholder, property));

    private static readonly Parser<char, PlaceholderPropertyFactory> Line =
        PlaceholderSubProperty<LineProperty>(PlaceholderProperty.Line, (placeholder, property)
            => new PlaceholderLineParameter(placeholder, property));

    private static readonly Parser<char, PlaceholderPropertyFactory> Offset =
        PlaceholderSubProperty<OffsetProperty>(PlaceholderProperty.Offset, (placeholder, property)
            => new PlaceholderOffsetParameter(placeholder, property));

    private static readonly Parser<char, PlaceholderPropertyFactory> Lenght =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Lenght)
            .Select<PlaceholderPropertyFactory>(property =>
                placeholder => new PlaceholderLenghtParameter(placeholder, property));

    public static readonly Parser<char, IRuleParameter> PlaceholderPropertyParameter =
        ParametersParser.PlaceholderParameter.Then
        (
            CommonParser.Dote.Then(Parser.OneOf(Lenght.Try(), Input.Try(), Column.Try(), Offset.Try(), Line)),
            (placeholder, ruleBuilder) => ruleBuilder(placeholder)
        ).Cast<IRuleParameter>();

    private static Parser<char, PlaceholderPropertyFactory> PlaceholderSubProperty<TEnum>(PlaceholderProperty property,
        Func<PlaceholderParameter, TEnum, IPlaceholderPropertyRuleParameter> func)
        where TEnum : struct, Enum
        => Parsers.Parsers.EnumValue(property)
            .Then(CommonParser.Dote)
            .Then(Parser.CIEnum<TEnum>())
            .Select<PlaceholderPropertyFactory>(enumValue => placeholder => func(placeholder, enumValue));
}