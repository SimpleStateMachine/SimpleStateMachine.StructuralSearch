using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal static class PlaceholderPropertyParser
{
    internal delegate IPlaceholderRelatedRuleParameter BuildPlaceholderRule(PlaceholderParameter parameter);

    private static Parser<char, BuildPlaceholderRule> PlaceholderSubProperty<TEnum>(PlaceholderProperty property, Func<PlaceholderParameter, TEnum, IPlaceholderRelatedRuleParameter> func)
        where TEnum : struct, Enum
        => Parsers.Parsers.EnumValue(property)
            .Then(CommonParser.Dote)
            .Then(Parser.CIEnum<TEnum>())
            .Select<BuildPlaceholderRule>(enumValue => placeholder => func(placeholder, enumValue))
            .Try();

    private static readonly Parser<char, BuildPlaceholderRule> File =
        Parsers.Parsers.EnumValue(PlaceholderProperty.File)
            .Then(CommonParser.Dote)
            .Then(Grammar.Identifier)
            .Select<BuildPlaceholderRule>(propertyName => placeholder => new PlaceholderFileParameter(placeholder, propertyName))
            .Try();

    private static readonly Parser<char, BuildPlaceholderRule> Column =
        PlaceholderSubProperty<ColumnProperty>(PlaceholderProperty.Column, (placeholder, property)
            => new PlaceholderColumnParameter(placeholder, property));

    private static readonly Parser<char, BuildPlaceholderRule> Line =
        PlaceholderSubProperty<LineProperty>(PlaceholderProperty.Line, (placeholder, property)
            => new PlaceholderLineParameter(placeholder, property));

    private static readonly Parser<char, BuildPlaceholderRule> Offset =
        PlaceholderSubProperty<OffsetProperty>(PlaceholderProperty.Offset, (placeholder, property)
            => new PlaceholderOffsetParameter(placeholder, property));

    private static readonly Parser<char, BuildPlaceholderRule> Lenght =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Lenght)
            .Select<BuildPlaceholderRule>(property => placeholder => new PlaceholderLenghtParameter(placeholder, property))
            .Try();

    internal static readonly Parser<char, BuildPlaceholderRule> PlaceholderPropertyParameter =
        CommonParser.Dote.Then(Parser.OneOf(Lenght, File, Column, Offset, Line))
            .Try()
            .Optional()
            .Select<BuildPlaceholderRule>(property => 
                placeholder => property.HasValue ? property.Value(placeholder) : placeholder);
}