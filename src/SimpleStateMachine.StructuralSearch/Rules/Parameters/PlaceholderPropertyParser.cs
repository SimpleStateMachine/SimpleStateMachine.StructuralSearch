using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
       
    public static class PlaceholderPropertyParser
    {
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> File =
            Parsers.EnumValue(PlaceholderProperty.File, true)
                .Then(CommonParser.Dote)
                .Then(Parser.CIEnum<Rules.FileProperty>())
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                        new PlaceholderFileParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Column =
            Parsers.EnumValue(PlaceholderProperty.Column, true)
                .Then(CommonParser.Dote)
                .Then(Parser.CIEnum<Rules.ColumnProperty>())
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderColumnParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Line =
            Parsers.EnumValue(PlaceholderProperty.Line, true)
                .Then(CommonParser.Dote)
                .Then(Parser.CIEnum<Rules.LineProperty>())
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderLineParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Offset =
            Parsers.EnumValue(PlaceholderProperty.Offset, true)
                .Then(CommonParser.Dote)
                .Then(Parser.CIEnum<Rules.OffsetProperty>())
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderOffsetParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Lenght =
            Parsers.EnumValue(PlaceholderProperty.Lenght, true)
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderLenghtParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> PlaceholderPropertyParameter =
            CommonParser.Dote.Then(Parser.OneOf(Lenght, File, Column, Offset, Line)).Optional()
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder =>
                    property.HasValue ? property.Value(placeholder) : placeholder))
                .Try();
        
        // public static readonly Parser<char, IRuleParameter> PlaceholderPropertyParameter =
        //     CommonTemplateParser.Placeholder.Before(CommonParser.Dote)
        //         .Select(name => new PlaceholderParameter(name))
        //         .Then(Parser.OneOf(Lenght, File, Column, Offset, Line),
        //             (placeholder, func) => func(placeholder))
        //         .TrimStart()
        //         .Try();
    }
}