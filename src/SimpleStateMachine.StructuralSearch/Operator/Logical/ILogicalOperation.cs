using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

public interface ILogicalOperation
{
    bool IsApplicableForPlaceholder(string placeholderName);
    bool Execute(ref IParsingContext context);
}