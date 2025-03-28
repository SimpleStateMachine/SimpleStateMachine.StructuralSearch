using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

public class EmptyLogicalOperation : ILogicalOperation
{
    public bool Execute(ref IParsingContext context)
        => true;
}