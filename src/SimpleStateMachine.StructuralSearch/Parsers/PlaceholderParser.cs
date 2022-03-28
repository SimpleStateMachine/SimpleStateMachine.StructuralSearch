﻿using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public class PlaceholderParser : ParserWithLookahead<char, string>, IContextDependent
    {
        private ParsingContext _context;

        public PlaceholderParser(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override Parser<char, string> BuildParser(Func<Parser<char, string>> next,
            Func<Parser<char, string>> nextNext)
        {
            var _next = next();
            var _nextNext = nextNext() ?? Parser<char>.End.ThenReturn(string.Empty);
            var lookahead = Parsers.Lookahead(_next.Then(_nextNext, (s1, s2) =>
            {
                OnLookahead = () => new List<LookaheadResult<char, string>>
                {
                    new(_next, s1, s1.Length),
                    new(_nextNext, s2, s2.Length),
                };
                return Unit.Value;
            }).Try());

            var anyString = CommonTemplateParser.AnyCharWithPlshd
                .AtLeastOnceAsStringUntil(lookahead);

            var simpleString = CommonTemplateParser.StringWithPlshd;
            var token = Parser.OneOf(simpleString, CommonParser.WhiteSpaces).Try();
            Parser<char, string> term = null;

            var parenthesised = Parsers.BetweenOneOfChars(x => Parsers.Stringc(x),
                expr: Parser.Rec(() => term),
                Constant.AllParenthesised).JoinToString();

            term = Parser.OneOf(parenthesised, token).Many().JoinToString();

            //parenthesised and tokens and whiteSpaces
            var prdsAndTokens = Parser.OneOf(parenthesised, token)
                .Until(lookahead)
                .JoinToString()
                .Try();

            var parser = prdsAndTokens.Or(anyString);
            return parser;
        }

        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expected,
            out string result)
        {
            bool res;
            
            // No use look-ahead if placeholder is already defined
            if (_context.TryGet(Name, out var value))
            {
                res = Parser.String(value).TryParse(ref state, ref expected, out result);
            }
            else
            {
                res = base.TryParse(ref state, ref expected, out result);
                if (res)
                    _context.AddPlaceholder(Name, result);
            }
            
            return res;
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
        public void SetContext(ParsingContext context)
        {
            _context = context;
        }
    }
}