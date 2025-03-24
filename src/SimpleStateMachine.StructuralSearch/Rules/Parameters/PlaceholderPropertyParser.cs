using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal static class PlaceholderPropertyParser
{
    private static Parser<char, Func<PlaceholderParameter, IRuleParameter>> PlaceholderSubProperty<TEnum>(PlaceholderProperty property, Func<PlaceholderParameter, TEnum, IRuleParameter> func)
        where TEnum : struct, Enum
        => Parsers.Parsers.EnumValue(property)
            .Then(CommonParser.Dote)
            .Then(Parser.CIEnum<TEnum>())
            .Select(enumValue => new Func<PlaceholderParameter, IRuleParameter>(placeholder => func(placeholder, enumValue)))
            .Try();

    private static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> File =
        Parsers.Parsers.EnumValue(PlaceholderProperty.File)
            .Then(CommonParser.Dote)
            .Then(Grammar.Identifier)
            .Select(propertyName => new Func<PlaceholderParameter, IRuleParameter>(placeholder => new PlaceholderFileParameter(placeholder, propertyName)))
            .Try();

    private static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Column =
        PlaceholderSubProperty<ColumnProperty>(PlaceholderProperty.Column, (placeholder, property)
            => new PlaceholderColumnParameter(placeholder, property));

    private static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Line =
        PlaceholderSubProperty<LineProperty>(PlaceholderProperty.Line, (placeholder, property)
            => new PlaceholderLineParameter(placeholder, property));

    private static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Offset =
        PlaceholderSubProperty<OffsetProperty>(PlaceholderProperty.Offset, (placeholder, property)
            => new PlaceholderOffsetParameter(placeholder, property));

    private static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Lenght =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Lenght)
            .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => new PlaceholderLenghtParameter(placeholder, property)))
            .Try();

    public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> PlaceholderPropertyParameter =
        CommonParser.Dote.Then(Parser.OneOf(Lenght, File, Column, Offset, Line))
            .Try()
            .Optional()
            .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder =>
                property.HasValue ? property.Value(placeholder) : placeholder));

    // public static readonly Parser<char, IRuleParameter> PlaceholderPropertyParameter =
    //     CommonTemplateParser.Placeholder.Before(CommonParser.Dote)
    //         .Select(name => new PlaceholderParameter(name))
    //         .Then(Parser.OneOf(Lenght, File, Column, Offset, Line),
    //             (placeholder, func) => func(placeholder))
    //         .TrimStart()
    //         .Try();
}