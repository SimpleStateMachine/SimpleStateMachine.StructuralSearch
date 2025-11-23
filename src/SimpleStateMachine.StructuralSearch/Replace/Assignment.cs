using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class Assignment(PlaceholderParameter placeholderParameter, IParameter newValue)
{
    public IPlaceholder GetPlaceholder(ref IParsingContext context) => placeholderParameter.GetPlaceholder(ref context);

    public string GetValue(ref IParsingContext context)
        => newValue.GetValue(ref context);
    
    public override string ToString()
        => $"{placeholderParameter}{Constant.Space}{Constant.Should}{Constant.Space}{newValue}";
}