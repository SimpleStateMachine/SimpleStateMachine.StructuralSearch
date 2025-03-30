using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal class PlaceholderParser : ParserWithLookahead<char, string>, IContextDependent
{
    internal static readonly IReadOnlySet<char> InvalidStringLiteralChars = new HashSet<char>(Constant.AllParenthesis)
    {
        Constant.CarriageReturn,
        Constant.LineFeed,
        Constant.Space
    };

    internal static readonly Parser<char, char> StringLiteralChar = Parser.AnyCharExcept(InvalidStringLiteralChars);

    private readonly string _name;
    private IParsingContext? _context;

    public PlaceholderParser(string name)
    {
        _name = name;
    }

    private IParsingContext Context => _context ?? throw new ArgumentNullException(nameof(_context));

    protected override Parser<char, string> BuildParser(Func<Parser<char, string>?> next, Func<Parser<char, string>?> nextNext)
    {
        var nextParser = next();
        var nextNextParser = nextNext();

        Parser<char, Unit> lookahead;
        if (nextNextParser is not null && nextParser is not null)
        {
            lookahead = nextParser.Then(nextNextParser, (s1, s2) =>
            {
                OnLookahead = () => new List<LookaheadResult<char, string>>
                {
                    new(nextParser, s1, s1.Length),
                    new(nextNextParser, s2, s2.Length),
                };
                return Unit.Value;
            }).Try().Lookahead();
        }
        else if (nextParser is not null)
        {
            lookahead = nextParser.Select(s =>
            {
                OnLookahead = () => new List<LookaheadResult<char, string>>
                {
                    new(nextParser, s, s.Length)
                };
                return Unit.Value;
            }).Try().Lookahead();
        }
        else
        {
            lookahead = Parser.EndOfLine.Select(x => Unit.Value);
        }

        return CreateParser(lookahead);


        //parenthesised and tokens and whiteSpaces
        // var prdsAndTokens = Parser.OneOf(parenthesised, token)
        //     .AtLeastOnceUntil(lookahead)
        //     .JoinToString()
        //     .Try();

        // var parser = prdsAndTokens.Or(anyString);
    }

    // internal static Parser<char, string> GreedyUntil(Parser<char, Unit> terminator)
    // {
    //     var term = Parser.AnyCharExcept(Constant.AllParenthesis)
    //         .AtLeastOnceAsStringUntil(terminator)
    //         .WithDebug("term");
    //
    //     Parser<char, string>? parser = null;
    //     
    //     var bracketed = Parser.Rec(() => parser!)
    //         .ManyString()
    //         .BetweenAnyParentheses((l, inner, r) => $"{l}{inner}{r}")
    //         .WithDebug("bracketed");
    //     
    //     parser = Parser.OneOf(bracketed.Try(), term);
    //     
    //     return parser.AtLeastOnceUntil(terminator).JoinToString();
    //     
    //     // var term = Parser.AnyCharExcept(Constant.AllParenthesis).Until(terminator)
    //     //     .Select(x => new string(x.ToArray()));
    //     //
    //     // Parser<char, string>? parser = null;
    //     //
    //     // var bracketed = Parser.OneOf(Parser.Rec(() => parser!), term)
    //     //     .ManyString()
    //     //     .BetweenAnyParentheses((c1, s, c2) => $"{c1}{s}{c2}").WithDebug();
    //     //
    //     // parser = Parser.OneOf(bracketed.Try(), term);
    //     // return parser.AtLeastOnceUntil(terminator).JoinToString();
    // }
    
    // internal static Parser<char, string> GreedyUntil(Parser<char, Unit> terminator)
    // {
    //     var word = StringLiteralChar.AtLeastOnceString().WithDebug();
    //     var whitespace = Parser.OneOf(Constant.WhitespaceChars).AtLeastOnceString().WithDebug();
    //     
    //     // var word = StringLiteralChar.AtLeastOnceAsStringUntil(terminator).WithDebug();
    //     // var whitespace = Parser.OneOf(Constant.WhitespaceChars).AtLeastOnceAsStringUntil(terminator).WithDebug();
    //     var term = Parser.OneOf(word.Try(), whitespace);
    //
    //     Parser<char, string>? parser = null;
    //     
    //     var bracketed = Parser.OneOf(Parser.Rec(() => parser!), term)
    //         .ManyString()
    //         .BetweenAnyParentheses((c1, s, c2) => $"{c1}{s}{c2}").WithDebug();
    //
    //     parser = Parser.OneOf(bracketed.Try(), term);
    //     return parser.ManyString().Before(terminator.Try());
    // }
    
    internal static Parser<char, string> CreateParser(Parser<char, Unit> terminator)
    {
        var anyString = StringLiteralChar
            .AtLeastOnceAsStringUntil(terminator);

        var simpleString = StringLiteralChar.AtLeastOnceString();
        var token = Parser.OneOf(simpleString, Grammar.WhiteSpaces).Try();
        Parser<char, string>? term = null;

        var parenthesised = Parser.Rec(() => term ?? throw new ArgumentNullException(nameof(term)))
            .BetweenAnyParentheses((l, s, r) => $"{l}{s}{r}");

        term = Parser.OneOf(parenthesised, token).Many().JoinToString();

        //parenthesised and tokens and whiteSpaces
        var prdsAndTokens = Parser.OneOf(parenthesised, token)
            .AtLeastOnceUntil(terminator)
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
        if (Context.TryGetValue(_name, out var placeholder))
        {
            res = Parser.String(placeholder.Value).TryParse(ref state, ref expected, out result!);
        }
        else
        {
            res = LookaheadParser.Value.Match().TryParse(ref state, ref expected, out var match);
            result = match.Value;
            if (res)
            {
                var placeholderObj = new Placeholder
                (
                    name: _name,
                    match: match
                );

                Context[_name] = placeholderObj;

                res = Context.FindRules
                    .Where(r => r.IsApplicableForPlaceholder(_name))
                    .All(r => r.Execute(ref _context!));

                if (!res)
                    Context.Remove(_name);
            }
        }

        return res;
    }

    public void SetContext(ref IParsingContext context)
    {
        _context = context;
    }
}