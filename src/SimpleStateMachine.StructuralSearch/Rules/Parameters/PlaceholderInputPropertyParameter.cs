using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderInputPropertyParameter : IPlaceholderPropertyRuleParameter
{
    private readonly string _propertyName;

    public PlaceholderInputPropertyParameter(PlaceholderParameter parameter, string propertyName)
    {
        Placeholder = parameter;
        _propertyName = propertyName;
    }

    public PlaceholderParameter Placeholder { get; }

    public string GetValue(ref IParsingContext context)
        => context.Input.GetProperty(_propertyName);

    public override string ToString()
        => $"{Placeholder}{Constant.Dote}{PlaceholderProperty.Input}{Constant.Dote}{_propertyName}";
}