using System;

namespace SimpleStateMachine.StructuralSearch.Rules;

public class UnaryLogicalRule : ILogicalRule
{
    public UnaryRuleType Type { get; }

    public ILogicalRule Parameter { get; }

    public UnaryLogicalRule(UnaryRuleType type, ILogicalRule parameter)
    {
        Type = type;
        Parameter = parameter;
    }

    public bool Execute()
    {
        var result = Parameter.Execute();

        return Type switch
        {
            UnaryRuleType.Not => !result,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override string ToString()
    {
        return $"{Type}{Constant.Space}{Parameter}";
    }
}