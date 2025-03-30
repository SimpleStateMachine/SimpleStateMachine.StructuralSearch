using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class Assignment
{
    private readonly PlaceholderParameter _placeholder;
    private readonly IParameter _newValue;

    public Assignment(PlaceholderParameter placeholder, IParameter newValue)
    {
        _placeholder = placeholder;
        _newValue = newValue;
    }

    public override string ToString()
        => $"{_placeholder}{Constant.Space}{Constant.Should}{Constant.Space}{_newValue}";
}