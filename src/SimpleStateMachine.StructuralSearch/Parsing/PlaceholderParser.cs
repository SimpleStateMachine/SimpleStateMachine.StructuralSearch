using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Parsing;

internal class PlaceholderParser(string name) : ParserWithLookahead<char, string>, IContextDependent
{
    private static readonly IReadOnlySet<char> InvalidStringLiteralChars = new HashSet<char>(Constant.AllParenthesis)
    {
        Constant.CarriageReturn,
        Constant.LineFeed
    };

    private static readonly Parser<char, char> StringLiteralChar = Parser.AnyCharExcept(InvalidStringLiteralChars);

    private IParsingContext? _context;

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
            lookahead = Parser<char>.End.Select(x => Unit.Value);
        }

        return CreateParser(lookahead);
    }

    internal static Parser<char, string> CreateParser(Parser<char, Unit> terminator)
    {
        var anyString = StringLiteralChar.AtLeastOnceAsStringUntil(terminator);

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
        if (Context.TryGetValue(name, out var placeholder))
        {
            res = Parser.String(placeholder.Value).TryParse(ref state, ref expected, out result!);
        }
        else
        {
            res = LookaheadParser.Value.Match().TryParse(ref state, ref expected, out var match);
            result = match.Value;
            if (res)
            {
                var placeholderObj = new Placeholder.Placeholder
                (
                    name: name,
                    match: match
                );

                Context[name] = placeholderObj;

                res = Context.FindRules
                    .Where(r => r.IsApplicableForPlaceholder(name))
                    .All(r => r.Execute(ref _context!));

                if (!res)
                    Context.Remove(name);
            }
        }

        return res;
    }

    public void SetContext(ref IParsingContext context)
    {
        _context = context;
    }
}