using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal static class PlaceholderPropertyParser
{
    public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> File =
        Parsers.Parsers.EnumValue(PlaceholderProperty.File, true)
            .Then(CommonParser.Dote)
            .Then(CommonParser.Identifier)
            .Select(propertyName => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                new PlaceholderFileParameter(placeholder, propertyName)))
            .Try();
        
    public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Column =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Column, true)
            .Then(CommonParser.Dote)
            .Then(Parser.CIEnum<ColumnProperty>())
            .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                new PlaceholderColumnParameter(placeholder, property)))
            .Try();
        
    public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Line =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Line, true)
            .Then(CommonParser.Dote)
            .Then(Parser.CIEnum<LineProperty>())
            .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                new PlaceholderLineParameter(placeholder, property)))
            .Try();
        
    public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Offset =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Offset, true)
            .Then(CommonParser.Dote)
            .Then(Parser.CIEnum<OffsetProperty>())
            .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                new PlaceholderOffsetParameter(placeholder, property)))
            .Try();
        
    public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Lenght =
        Parsers.Parsers.EnumValue(PlaceholderProperty.Lenght, true)
            .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                new PlaceholderLenghtParameter(placeholder, property)))
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