using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Operator.Logical;

namespace SimpleStateMachine.StructuralSearch.CustomParsers;

internal class FindParser : IFindParser
{
    private FindTemplateParser Parser { get; }

    public FindParser(FindTemplateParser parser)    
    {
        Parser = parser;
    }

    public List<FindParserResult> Parse(IInput input, params ILogicalOperation[] findRules)
    {
        List<FindParserResult> matches = [];
        StringBuilder res = new();

        IParsingContext context = new ParsingContext(input, findRules);
        Parser.SetContext(ref context);

        var parsingContext = context;
        var parser = Parser.Select(x => string.Join(string.Empty, x))
            .Match()
            .ThenInvoke(match =>
            {
                var placeholders = context.ToDictionary();
                context.Clear();
                matches.Add(new FindParserResult(match, placeholders));
            })
            .ThenReturn(Unit.Value)
            .Try();

        var empty = Parser<char>.Any
            .ThenInvoke(x =>
            {
                res.Append(x);
                parsingContext.Clear();
            })
            .ThenReturn(Unit.Value);

        using var textReader = context.Input.ReadData();
        Pidgin.Parser.OneOf(parser, empty).Many().Parse(textReader);
        return matches.OrderBy(x=> x.Match.Offset.Start).ToList();
    }
}