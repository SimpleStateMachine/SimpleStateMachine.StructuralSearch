using System.Collections.Generic;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Extensions;

public static class FindParserExtensions
{
    public static IEnumerable<FindParserResult> ParseString(this IFindParser findParser, string inputString)
    {
        var input = Input.Input.String(inputString);
        return findParser.Parse(input);
    }

    public static IEnumerable<FindParserResult> ParseFile(this IFindParser findParser, FileInfo fileInfo)
    {
        var input = Input.Input.File(fileInfo);
        return findParser.Parse(input);
    }
}