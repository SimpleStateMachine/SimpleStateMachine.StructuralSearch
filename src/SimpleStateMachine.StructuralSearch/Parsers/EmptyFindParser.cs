using System;
using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Input;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal sealed class EmptyFindParser: IFindParser
{
    public static readonly EmptyFindParser Value = new ();

    public List<FindParserResult> Parse(IInput input) 
        => Array.Empty<FindParserResult>().ToList();
}