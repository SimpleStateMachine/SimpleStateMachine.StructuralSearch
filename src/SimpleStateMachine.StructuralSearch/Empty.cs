using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch;

public sealed class EmptyFindParser: IFindParser
{
    public static readonly EmptyFindParser Value = new ();

    public List<FindParserResult> Parse(IInput input) 
        => Array.Empty<FindParserResult>().ToList();
}