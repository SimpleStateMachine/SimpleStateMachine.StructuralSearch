﻿using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal class PlaceholderParser : ParserWithLookahead<char, string>, IContextDependent
{
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

        var anyString = CommonTemplateParser.StringLiteralChar
            .AtLeastOnceAsStringUntil(lookahead);

        var simpleString = CommonTemplateParser.StringLiteral;
        var token = Parser.OneOf(simpleString, CommonParser.WhiteSpaces).Try();
        Parser<char, string>? term = null;

        var parenthesised = Parsers.BetweenOneOfChars
        (
            leftRight: x => Parser.Char(x).AsString(),
            expr: Parser.Rec(() => term ?? throw new ArgumentNullException(nameof(term))),
            values: Constant.AllParentheses
        ).JoinToString();

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
                var placeholderObj = new Placeholder.Placeholder
                (
                    name: _name,
                    match: match
                );

                Context.Add(_name, placeholderObj);

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