using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

public interface ILogicalOperation
{
    bool Execute(ref IParsingContext context);
}