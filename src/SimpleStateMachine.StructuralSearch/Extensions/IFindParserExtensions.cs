using System.Collections.Generic;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Extensions;

public static class FindParserExtensions
{
    public static IEnumerable<FindParserResult> ParseString(this IFindParser findParser, string inputString)
    {
        var input = Input.String(inputString);
        IParsingContext context = new ParsingContext(input);
        return findParser.Parse(ref context);
    }
    
    public static IEnumerable<FindParserResult> ParseFile(this IFindParser findParser, FileInfo fileInfo)
    {
        var input = Input.File(fileInfo);
        IParsingContext context = new ParsingContext(input);
        return findParser.Parse(ref context);
    }
}