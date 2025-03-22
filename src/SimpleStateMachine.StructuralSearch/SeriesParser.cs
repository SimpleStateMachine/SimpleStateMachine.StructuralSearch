using System.Collections.Generic;
using System.Linq;
using Pidgin;

#pragma warning disable CS9074 // The 'scoped' modifier of parameter doesn't match overridden or implemented member.

namespace SimpleStateMachine.StructuralSearch;

internal class SeriesParser : Parser<char, IEnumerable<string>>, IContextDependent
{
    private readonly IEnumerable<Parser<char, string>> _parsers;

    public SeriesParser(IEnumerable<Parser<char, string>> parsers)
    {
        _parsers = parsers;
            
        InitializeLookaheadParsers();
    }

    private void InitializeLookaheadParsers()
    {
        var count = _parsers.Count();
            
        for (var i = count-1; i >= 0 ; i--)
        {
            var parser = _parsers.ElementAt(i);
            if (parser is not ParserWithLookahead<char, string> lookaheadParser) continue;
                
            var number = i;
            lookaheadParser.Lookahead(() => _parsers.ElementAtOrDefault(number + 1), () =>
                _parsers.ElementAtOrDefault(number + 2));
        }
    }

    public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expected, out IEnumerable<string> result)
    {
        var results = new List<string>();
        var count = _parsers.Count();

        for (int i = 0; i < count; i++)
        {
            var parser = _parsers.ElementAt(i);
                
            if (!parser.TryParse(ref state, ref expected, out var parserResult))
            {
                result = results;
                return false;
            }
                
            results.Add(parserResult);

            SkipLookedParsers(parser, ref state);

            void SkipLookedParsers(Parser<char, string> parser, ref ParseState<char> state)
            {
                if (parser is not ParserWithLookahead<char, string> lookaheadParser ||
                    lookaheadParser is { OnLookahead: null })
                    return;

                var lookaheadResults = lookaheadParser.OnLookahead.Invoke().ToArray();

                foreach (var result in lookaheadResults)
                {
                    results.Add(result.Result);
                    state.Advance(result.TokensCount);
                    i++;
                }

                //recursion
                foreach (var result in lookaheadResults)
                {
                    SkipLookedParsers(result.Parser, ref state);
                }
            }
        }

        result = results;
        return true;
    }
        
    public void SetContext(ref IParsingContext parsingContext)
    {
        foreach (var parser in _parsers)
        {
            if (parser is IContextDependent element)
            {
                element.SetContext(ref parsingContext);
            }
        }
    }
}