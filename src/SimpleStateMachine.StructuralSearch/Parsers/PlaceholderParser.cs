﻿using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public class PlaceholderParser : ParserWithLookahead<char, string>, IContextDependent
    {
        private IParsingContext _context;

        public PlaceholderParser(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override Parser<char, string> BuildParser(Func<Parser<char, string>?> next,
            Func<Parser<char, string>?> nextNext)
        {
            var _next = next();
            var _nextNext = nextNext();
                // ?? Parser.OneOf(Parser<char>.End.ThenReturn(string.Empty), Parser<char>.Any.ThenReturn(string.Empty));
                //  Parser<char>.End.ThenReturn(string.Empty)
            
            Parser<char, Unit> lookahead;
            if (_nextNext is not null)
            {
                lookahead = Parsers.Lookahead(_next.Then(_nextNext, (s1, s2) =>
                {
                    OnLookahead = () => new List<LookaheadResult<char, string>>
                    {
                        new(_next, s1, s1.Length),
                        new(_nextNext, s2, s2.Length),
                    };
                    return Unit.Value;
                }).Try());
            }
            else
            {
                lookahead = Parsers.Lookahead(_next.Select(s =>
                {
                    OnLookahead = () => new List<LookaheadResult<char, string>>
                    {
                        new(_next, s, s.Length)
                    };
                    return Unit.Value;
                }).Try());  
            }


            var anyString = CommonTemplateParser.AnyCharWithPlshd
                .AtLeastOnceAsStringUntil(lookahead);

            var simpleString = CommonTemplateParser.StringWithPlshd;
            var token = Parser.OneOf(simpleString, CommonParser.WhiteSpaces).Try();
            Parser<char, string> term = null;

            var parenthesised = Parsers.BetweenOneOfChars(x => Parser.Char(x).AsString(),
                expr: Parser.Rec(() => term),
                Constant.AllParenthesised).JoinToString();

            term = Parser.OneOf(parenthesised, token).Many().JoinToString();

            //parenthesised and tokens and whiteSpaces
            var prdsAndTokens = Parser.OneOf(parenthesised, token)
                .AtLeastOnceUntil(lookahead)
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
            if (_context.TryGetPlaceholder(Name, out var placeholder))
            {
                res = Parser.String(placeholder.Value).TryParse(ref state, ref expected, out result);
            }
            else
            {
                res = parser.Value.Match().TryParse(ref state, ref expected, out var match);
                result = match.Value;
                if (res)
                {
                    _context.AddPlaceholder(new Placeholder(
                        context: _context,
                        name: Name,
                        match: match));
                }
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
        public void SetContext(ref IParsingContext context)
        {
            _context = context;
        }
    }
}