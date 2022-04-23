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

        public string Parse(ref IParsingContext context, IInput input)
        {
            StringBuilder res = new StringBuilder();
            Parser.SetContext(ref context);
            var parser = Parser.Select(x => string.Join(string.Empty, x)).Match().Try();
            var empty = Pidgin.Parser<char>.Any.Select(x=>
            {
                res.Append(x);
                return string.Empty;
            }).ThenReturn(Match.EmptyMatchString);
            var parse = Pidgin.Parser.OneOf(parser, empty)
                .Many();
                
            var result = input.Parse(parse);
            var t = result.Value.Where(x => !x.IsEmpty());
            // return result.Success ? result.Value.JoinToString() : string.Empty;
            return string.Empty;
        }
    }
}