﻿using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Operator.Logical;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

internal class ReplaceRule : IReplaceRule
{
    public static readonly EmptyReplaceRule Empty = new();

    private readonly ILogicalOperation _condition;

    public ReplaceRule(ILogicalOperation condition, List<Assignment> assignments)
    {
        _condition = condition;
        Assignments = assignments;
    }

    public IEnumerable<Assignment> Assignments { get; }

    public bool IsMatch(ref IParsingContext context)
        => _condition.Execute(ref context);

    public override string ToString()
    {
        var builder = new StringBuilder();
        var conditionStr = _condition.ToString();

        if (!string.IsNullOrEmpty(conditionStr))
            builder.Append(conditionStr).Append(Constant.Space);

        builder.AppendJoin(Constant.Comma, Assignments);
        return builder.ToString();
    }
}