using System.Collections.Generic;
using System.Text;
using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class ReplaceRule(IReplaceCondition condition, List<Assignment> assignments) : IReplaceRule
{
    public static readonly EmptyReplaceRule Empty = new();

    public List<Assignment> Assignments { get; } = assignments;

    public bool IsMatch(ref IParsingContext context)
        => condition.Execute(ref context);

    public override string ToString()
    {
        var builder = new StringBuilder();
        var conditionStr = condition.ToString();

        if (!string.IsNullOrEmpty(conditionStr))
            builder.Append(conditionStr).Append(Constant.Space);

        builder.AppendJoin(Constant.Comma, Assignments);
        return builder.ToString();
    }
}