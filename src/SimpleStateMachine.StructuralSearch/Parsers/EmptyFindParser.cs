using System;
using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal sealed class EmptyFindParser : IFindParser
{
    public static readonly EmptyFindParser Value = new();

    public List<FindParserResult> Parse(IInput input, params IFindRule[] findRules)
        => Array.Empty<FindParserResult>().ToList();
}