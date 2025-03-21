using System;
using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch;

public interface IFindParser
{
    IEnumerable<FindParserResult> Parse(ref IParsingContext context);
}
    
public class EmptyFindParser: IFindParser
{
    public IEnumerable<FindParserResult> Parse(ref IParsingContext context) 
        => Array.Empty<FindParserResult>();
}