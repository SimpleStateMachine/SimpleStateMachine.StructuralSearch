using System;
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
            var nextParser = next();
            var nextNextParser = nextNext();

            if (nextParser is null)
                return EmptyParser.AlwaysNotCorrectString;
            
            Parser<char, Unit> lookahead;
            if (nextNextParser is not null)
            {
            
                lookahead = Parser.Lookahead(nextParser.Then(nextNextParser, (s1, s2) =>
                {
                    OnLookahead = () => new List<LookaheadResult<char, string>>
                    {
                        new(nextParser, s1, s1.Length),
                        new(nextNextParser, s2, s2.Length),
                    };
                    return Unit.Value;
                }).Try());
            }
            else
            {
                lookahead = Parser.Lookahead(nextParser.Select(s =>
                {
                    OnLookahead = () => new List<LookaheadResult<char, string>>
                    {
                        new(nextParser, s, s.Length)
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
        
        public void SetContext(ref IParsingContext context)
        {
            _context = context;
        }
    }
}