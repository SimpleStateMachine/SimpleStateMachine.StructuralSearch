using System;
using System.Collections;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public class PlaceholderParser : LookaheadParser<char, SourceMatch>
    {
        public string Name { get; }

        public PlaceholderParser(string name)
        {
            Name = name;
        }
        
        public override Parser<char, SourceMatch> BuildParser<Res1, Res2>(Func<Parser<char, Res1>> next,
            Func<Parser<char, Res2>> nextNext)
        {
            var _next = next();
            var _nextNext = nextNext();
            var lookahead = Parser.Lookahead(_next.Then(_nextNext).Try());
            var anyString = CommonTemplateParser.AnyCharWithPlshd
                .AtLeastOnceAsStringUntil(lookahead);
            
            var simpleString = CommonTemplateParser.StringWithPlshd.Labelled("simpleString");
            var token = Parser.OneOf(simpleString, CommonParser.WhiteSpaces).Try();
            Parser<char, string> term = null;

            var parenthesised = Parsers.BetweenOneOfChars(Parsers.Stringc,
                expr: Parser.Rec(() => term),
                Constant.AllParenthesised).JoinToString();
            
            term = Parser.OneOf(parenthesised, token).Many().JoinToString();
            
            //parenthesised and tokens and whiteSpaces
            var prdsAndTokens = Parser.OneOf(parenthesised, token)
                .Until(lookahead)
                .JoinToString()
                .Try();
            
            var parser = prdsAndTokens.Or(anyString).AsMatch();
            return parser;
        }


        // internal Parser<char, SourceMatch> GetParser()
        // {
        //     
        // }
        // public override Parser<char, SourceMatch> BuildParser<Res1, Res2>(Func<Parser<char, Res1>> next, Func<Parser<char, Res2>> nextNext)
        // {
        //     var _next = next();
        //     var _nextNext = nextNext();
        //     var lookahead = Parser.Lookahead(_next.Then(_nextNext).Try());
        //     var anyString = Parser<char>.Any.AtLeastOnceAsStringUntil(lookahead)
        //         .Try();
        //     
        //     var simpleString = CommonTemplateParser.StringWithoutParenthesisedAndWhiteSpaces;
        //     var token = Parser.OneOf(simpleString, CommonParser.WhiteSpaces)
        //         .AtLeastOnce();
        //     Parser<char, IEnumerable<string>> term = null;
        //     
        //     var parenthesised = Parsers.BetweenOneOfChars(Parsers.Stringc, 
        //         Parser.Rec(() => term), 
        //         Constant.AllParenthesised);
        //     
        //     term = Parser.OneOf(token, parenthesised).AtLeastOnce().MergerMany();
        //
        //     // var parser = Parser.OneOf(term.JoinToString(), anyString).AsMatch();
        //     var parser = term.JoinToString().AsMatch();
        //     return parser;
        // }
    }
}