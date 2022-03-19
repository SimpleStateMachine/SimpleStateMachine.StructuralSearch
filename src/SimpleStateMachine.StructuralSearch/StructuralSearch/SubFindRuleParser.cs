using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public static class SubRuleParser
    {
        public static readonly Parser<char, IRuleParameter> PlaceholderParameter =
            CommonTemplateParser.Placeholder
                .Select(x => new PlaceholderParameter(x))
                .As<char, PlaceholderParameter, IRuleParameter>()
                .TrimStart()
                .Try();

        public static readonly Parser<char, IRuleParameter> StringParameter =
            Parser.AnyCharExcept(Constant.DoubleQuotes)
                .AtLeastOnceString()
                .Between(CommonParser.DoubleQuotes)
                .Select(x => new StringParameter(x))
                .As<char, StringParameter, IRuleParameter>()
                .TrimStart()
                .Try();
        

        public static readonly Parser<char, IRuleParameter> Parameter =
            Parser.OneOf(PlaceholderPropertyParser.PlaceholderPropertyParameter, PlaceholderParameter, StringParameter);

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
            Parameter.AtLeastOnce();

        public static readonly Parser<char, SubRuleType> SubRuleType =
            Parsers.EnumExcept(true, Rules.SubRuleType.Is, Rules.SubRuleType.In)
                .TrimStart();

        public static readonly Parser<char, IRule> UnarySubRule =
            Parser.Map((type, param) => new UnarySubRule(type, param),
                    SubRuleType, Parameter)
                .As<char, UnarySubRule, IRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parsers.Enum<PlaceholderType>(true);

        public static readonly Parser<char, IRule> IsSubRule =
            Parser.Map((type, param) => new IsRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.Is, true)
                        .TrimStart(),
                    PlaceholderType)
                .As<char, IsRule, IRule>()
                .Try();

        public static readonly Parser<char, IRule> InSubRule =
            Parser.Map((type, param) => new InRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.In, true)
                        .TrimStart(), Parameters)
                .As<char, InRule, IRule>()
                .Try();
    }
    
    public static class PlaceholderPropertyParser
    {
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> File =
            Parsers.EnumValue(PlaceholderProperty.File, true)
                .Then(CommonParser.Dote)
                .Then(Parsers.Enum<FileProperty>(true))
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                        new PlaceholderFileParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Column =
            Parsers.EnumValue(PlaceholderProperty.Column, true)
                .Then(CommonParser.Dote)
                .Then(Parsers.Enum<ColumnProperty>(true))
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderColumnParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Line =
            Parsers.EnumValue(PlaceholderProperty.Line, true)
                .Then(CommonParser.Dote)
                .Then(Parsers.Enum<LineProperty>(true))
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderLineParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Offset =
            Parsers.EnumValue(PlaceholderProperty.Offset, true)
                .Then(CommonParser.Dote)
                .Then(Parsers.Enum<OffsetProperty>(true))
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderOffsetParameter(placeholder, property)))
                .Try();
        
        public static readonly Parser<char, Func<PlaceholderParameter, IRuleParameter>> Lenght =
            Parsers.EnumValue(PlaceholderProperty.Lenght, true)
                .Select(property => new Func<PlaceholderParameter, IRuleParameter>(placeholder => 
                    new PlaceholderLenghtParameter(placeholder, property)))
                .Try();

        public static readonly Parser<char, IRuleParameter> PlaceholderPropertyParameter =
            CommonTemplateParser.Placeholder.Before(CommonParser.Dote)
                .Select(name => new PlaceholderParameter(name))
                .Then(Parser.OneOf(Lenght, File, Column, Offset, Line),
                    (placeholder, func) => func(placeholder))
                .TrimStart()
                .Try();
    }
}