using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class NotOperation(ILogicalOperation parameter) : ILogicalOperation
{
    public bool IsApplicableForPlaceholder(string placeholderName)
        => parameter.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var value = parameter.Execute(ref context);
        return !value;
    }

    public override string ToString()
        => $"{Constant.Not}{Constant.Space}{parameter}";
}