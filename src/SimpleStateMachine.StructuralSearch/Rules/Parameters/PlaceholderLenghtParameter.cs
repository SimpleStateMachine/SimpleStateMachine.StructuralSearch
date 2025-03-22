using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderLenghtParameter: IPlaceholderRelatedRuleParameter
{
    private readonly PlaceholderParameter _placeholderParameter;
    private readonly PlaceholderProperty _property;

    public PlaceholderLenghtParameter(PlaceholderParameter parameter, PlaceholderProperty property)
    {
        _placeholderParameter = parameter;
        _property = property;
    }
        
    public string PlaceholderName => _placeholderParameter.PlaceholderName;
        
    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = _placeholderParameter.GetPlaceholder(ref context);
        return placeHolder.Lenght.ToString();
    }
        
    public override string ToString() 
        => $"{_placeholderParameter}{Constant.Dote}{_property}";
}