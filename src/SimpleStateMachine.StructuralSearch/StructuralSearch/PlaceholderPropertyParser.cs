// using System;
// using Pidgin;
// using SimpleStateMachine.StructuralSearch.CustomParsers;
// using SimpleStateMachine.StructuralSearch.Extensions;
// using SimpleStateMachine.StructuralSearch.Parameters;
// using SimpleStateMachine.StructuralSearch.Parameters.Types;
//
// namespace SimpleStateMachine.StructuralSearch.StructuralSearch;
//
// internal static class PlaceholderPropertyParser
// {
//     private delegate IPlaceholderProperty PlaceholderPropertyFactory(PlaceholderParameter parameter);
//
//     private static readonly Parser<char, PlaceholderPropertyFactory> Input =
//         Parsers.EnumValue(PlaceholderPositionType.Input)
//             .Then(CommonParser.Dote)
//             .Then(Grammar.Identifier)
//             .Select<PlaceholderPropertyFactory>(propertyName =>
//                 placeholder => new PlaceholderInput(placeholder, propertyName));
//
//     private static readonly Parser<char, PlaceholderPropertyFactory> Column =
//         PlaceholderSubProperty<ColumnProperty>(PlaceholderPositionType.Column, (placeholder, property)
//             => new PlaceholderColumnParameter(placeholder, property));
//
//     private static readonly Parser<char, PlaceholderPropertyFactory> Line =
//         PlaceholderSubProperty<PlaceholderPositionSubProperty>(PlaceholderPositionType.Line, (placeholder, property)
//             => new PlaceholderLineParameter(placeholder, property));
//
//     private static readonly Parser<char, PlaceholderPropertyFactory> Offset =
//         PlaceholderSubProperty<OffsetProperty>(PlaceholderPositionType.Offset, (placeholder, property)
//             => new PlaceholderPosition(placeholder, property));
//
//     private static readonly Parser<char, PlaceholderPropertyFactory> Lenght =
//         Parsers.EnumValue(PlaceholderPositionType.Lenght)
//             .Select<PlaceholderPropertyFactory>(property =>
//                 placeholder => new PlaceholderLenghtParameter(placeholder, property));
//
//     public static readonly Parser<char, IParameter> PlaceholderPropertyParameter =
//         ParametersParser.PlaceholderParameter.Then
//         (
//             CommonParser.Dote.Then(Parser.OneOf(Lenght.Try(), Input.Try(), Column.Try(), Offset.Try(), Line)),
//             (placeholder, ruleBuilder) => ruleBuilder(placeholder)
//         ).Cast<IParameter>();
//
//     private static Parser<char, PlaceholderPropertyFactory> PlaceholderSubProperty<TEnum>(PlaceholderPositionType property,
//         Func<PlaceholderParameter, TEnum, IPlaceholderProperty> func)
//         where TEnum : struct, Enum
//         => Parsers.EnumValue(property)
//             .Then(CommonParser.Dote)
//             .Then(Parser.CIEnum<TEnum>())
//             .Select<PlaceholderPropertyFactory>(enumValue => placeholder => func(placeholder, enumValue));
// }