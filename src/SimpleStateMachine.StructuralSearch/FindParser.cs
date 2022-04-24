using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using YamlDotNet.Core.Events;

namespace SimpleStateMachine.StructuralSearch
{
    public class FindParser : IFindParser
    {
        private SeriesParser Parser { get; }
        public FindParser(SeriesParser parser)    
        {
            Parser = parser;
        }

        public IEnumerable<FindParserMatch> Parse(ref IParsingContext context)
        {
            List<FindParserMatch> matches = new();
            StringBuilder res = new();
            Parser.SetContext(ref context);
            
            var parsingContext = context;
            var parser = Parser.Select(x => string.Join(string.Empty, x))
                .Match()
                .ThenInvoke(match =>
                {
                    var placeholders= parsingContext.Switch();
                    matches.Add(new FindParserMatch(match, placeholders));
                })
                .ThenReturn(Unit.Value)
                .Try();
            
            var empty = Parser<char>.Any
                .ThenInvoke(x =>
                {
                    res.Append(x);
                    parsingContext.Switch();
                })
                .ThenReturn(Unit.Value);
            
            context.Input.ParseBy(Pidgin.Parser.OneOf(parser, empty).Many());
            
            return matches.OrderBy(x=> x.Match.Offset.Start);
        }
    }
}