using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class ReplaceRule : IReplaceRule
{
    public static readonly EmptyReplaceRule Empty = new();

    private readonly IReplaceCondition _condition;

    public ReplaceRule(IReplaceCondition condition, List<Assignment> assignments)
    {
        _condition = condition;
        Assignments = assignments;
    }

    public List<Assignment> Assignments { get; }

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