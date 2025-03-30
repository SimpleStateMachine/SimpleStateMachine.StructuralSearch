using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class Assignment
{
    private readonly PlaceholderParameter _placeholderParameter;
    private readonly IParameter _newValue;

    public Assignment(PlaceholderParameter placeholderParameter, IParameter newValue)
    {
        _placeholderParameter = placeholderParameter;
        _newValue = newValue;
    }

    public IPlaceholder GetPlaceholder(ref IParsingContext context) => _placeholderParameter.GetPlaceholder(ref context);

    public string GetValue(ref IParsingContext context)
        => _newValue.GetValue(ref context);
    
    public override string ToString()
        => $"{_placeholderParameter}{Constant.Space}{Constant.Should}{Constant.Space}{_newValue}";
}