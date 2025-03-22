using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderFileParameter : IPlaceholderRelatedRuleParameter
{
    private readonly PlaceholderParameter _placeholderParameter;
    private readonly string _propertyName;

    public PlaceholderFileParameter(PlaceholderParameter parameter, string propertyName)
    {
        _placeholderParameter = parameter;
        _propertyName = propertyName;
    }

    public string PlaceholderName => _placeholderParameter.PlaceholderName;

    public string GetValue(ref IParsingContext context)
        => context.Input.GetProperty(_propertyName);

    public override string ToString() 
        => $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.File}{Constant.Dote}{_propertyName}";
}