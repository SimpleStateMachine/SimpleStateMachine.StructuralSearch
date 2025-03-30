using System;
using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Operator.Logical;

namespace SimpleStateMachine.StructuralSearch.CustomParsers;

internal sealed class EmptyFindParser : IFindParser
{
    public static readonly EmptyFindParser Value = new();

    public List<FindParserResult> Parse(IInput input, params ILogicalOperation[] findRules)
        => Array.Empty<FindParserResult>().ToList();
}