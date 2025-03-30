using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Operator.Logical;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class ReplaceCondition : IReplaceCondition
{
    private readonly ILogicalOperation? _logicalOperation;

    public ReplaceCondition(ILogicalOperation? logicalOperation)
    {
        _logicalOperation = logicalOperation;
    }

    public bool Execute(ref IParsingContext context)
        => _logicalOperation is null || _logicalOperation.Execute(ref context);

    public override string ToString() => 
        _logicalOperation is null 
            ? string.Empty 
            : $"{Constant.If}{Constant.Space}{_logicalOperation}{Constant.Space}{Constant.Then}";
}