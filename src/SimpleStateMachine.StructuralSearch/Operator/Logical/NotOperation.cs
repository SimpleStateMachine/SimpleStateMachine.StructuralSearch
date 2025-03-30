using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class NotOperation : ILogicalOperation
{
    private readonly ILogicalOperation _parameter;

    public NotOperation(ILogicalOperation parameter)
    {
        _parameter = parameter;
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameter.IsApplicableForPlaceholder(placeholderName);

    public bool Execute(ref IParsingContext context)
    {
        var value = _parameter.Execute(ref context);
        return !value;
    }

    public override string ToString()
        => $"{Constant.Not}{Constant.Space}{_parameter}";
}