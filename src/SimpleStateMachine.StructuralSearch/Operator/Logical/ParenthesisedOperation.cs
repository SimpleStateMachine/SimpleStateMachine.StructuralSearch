using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

public class ParenthesisedOperation(ILogicalOperation logicalOperation) : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => logicalOperation.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
        => logicalOperation.Execute(ref context);

    public override string ToString()
        => $"({logicalOperation})";
}