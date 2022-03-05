using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Sandbox.Extensions;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Custom
{
    public class PlaceholderParser: Parser<char, SourceMatch>, ILookaheadParser<char, SourceMatch>
    {
        public string Name { get; }
        
        public PlaceholderParser(string name)
        {
            Name = name;
        }

        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expecteds, out SourceMatch result)
        {
            throw new NotImplementedException();
        }
        
        public bool TryParse<Res1, Res2>(
            Parser<char, Res1> next, 
            Parser<char, Res2> nextNext, 
            ref ParseState<char> state, 
            ref PooledList<Expected<char>> expected,
            out SourceMatch result)
        {
            var lookahead = Parser.Lookahead(next.Then(nextNext).Try());
            var anyString= Parser<char>.Any.AtLeastOnceAsStringUntil(lookahead).Try();
            var token = Parser.OneOf(anyString, CommonParser.WhiteSpaces).AtLeastOnce();
            Parser<char, IEnumerable<string>> term = null;
            
            var parenthesised = Parsers.BetweenOneOfChars(Parsers.Stringc, 
                Parser.Rec(() => term), 
                Constant.AllParenthesised);
            
            term = Parser.OneOf(token, parenthesised).AtLeastOnce().MergerMany();
                        
            var parser = term.JoinToString().AsMatch();

            return parser.TryParse(ref state, ref expected, out result);
        }
        
    }
}