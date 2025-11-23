using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Operator.Logical;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class ReplaceCondition(ILogicalOperation? logicalOperation) : IReplaceCondition
{
    public bool Execute(ref IParsingContext context)
        => logicalOperation is null || logicalOperation.Execute(ref context);

    public override string ToString() => 
        logicalOperation is null 
            ? string.Empty 
            : $"{Constant.If}{Constant.Space}{logicalOperation}{Constant.Space}{Constant.Then}";
}