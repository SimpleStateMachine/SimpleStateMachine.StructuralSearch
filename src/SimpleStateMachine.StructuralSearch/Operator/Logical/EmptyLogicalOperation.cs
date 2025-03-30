using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class EmptyLogicalOperation : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;

    public bool Execute(ref IParsingContext context)
        => true;

    public override string ToString()
        => string.Empty;
}