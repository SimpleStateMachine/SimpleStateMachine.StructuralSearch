using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Parsing;

internal static class FindTemplateParser
{
    private static readonly Parser<char, Parser<char, string>> Placeholder =
        Grammar.Placeholder.Select(Parser<char, string> (name) => new PlaceholderParser(name));

    private static readonly Parser<char, Parser<char, string>> TemplateStringLiteral =
        Grammar.TemplateStringLiteral.AtLeastOnceString()
            .Select(Parser.String);

    private static readonly Parser<char, Parser<char, string>> WhiteSpaces =
        Grammar.WhiteSpaces.SelectToParser((_, parser) => parser);

    // template_component = placeholder | template_string_literal | whitespace
    private static readonly Parser<char, Parser<char, string>> TemplateComponent
        = Parser.OneOf(Placeholder, TemplateStringLiteral, WhiteSpaces);

    // template_between_parentheses = '(' template ')' | '{' template '}' | '[' template ']
    private static readonly Parser<char, IEnumerable<Parser<char, string>>> TemplateBetweenParentheses
        = Parser.Rec(() => Template ?? throw new ArgumentNullException(nameof(Template)))
            .Optional() // To support empty Parentheses
            .BetweenAnyParentheses((left, result, right) =>
            {
                var leftParser = Parser.Char(left).AsString();
                var rightParser = Parser.Char(right).AsString();
                var inner = result.GetValueOrDefault([]);
                return inner.Prepend(leftParser).Append(rightParser);
            });

    // template = (template_component | template_between_parentheses)+
    internal static readonly Parser<char, IEnumerable<Parser<char, string>>> Template =
        Parser.OneOf
        (
            TemplateBetweenParentheses, 
            TemplateComponent.Select<IEnumerable<Parser<char, string>>>(x => new List<Parser<char, string>> { x })
        ).AtLeastOnce().Select(list => list.SelectMany(x => x));
}