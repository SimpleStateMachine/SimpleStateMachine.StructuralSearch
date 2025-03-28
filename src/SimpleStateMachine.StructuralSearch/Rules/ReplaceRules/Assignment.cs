using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

internal class Assignment
{
    private readonly PlaceholderParameter Placeholder;
    private readonly IParameter _newValue;

    public Assignment(PlaceholderParameter placeholder, IParameter newValue)
    {
        Placeholder = placeholder;
        _newValue = newValue;
    }

    public override string ToString()
        => $"{Placeholder}{Constant.Space}{Constant.Should}{Constant.Space}{_newValue}";
}