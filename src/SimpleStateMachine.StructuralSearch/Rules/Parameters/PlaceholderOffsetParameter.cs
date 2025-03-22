using System;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules;

internal class PlaceholderOffsetParameter : IPlaceholderRelatedRuleParameter
{
    private readonly PlaceholderParameter _placeholderParameter;
    private readonly OffsetProperty _property;

    public PlaceholderOffsetParameter(PlaceholderParameter parameter, OffsetProperty property)
    {
        _placeholderParameter = parameter;
        _property = property;
    }
        
    public string PlaceholderName => _placeholderParameter.PlaceholderName;
        
    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = _placeholderParameter.GetPlaceholder(ref context);
        var offset = placeHolder.Offset;
            
        var value = _property switch
        {
            OffsetProperty.Start => offset.Start,
            OffsetProperty.End => offset.End,
            _ => throw new ArgumentOutOfRangeException(nameof(_property).FormatPrivateVar(), _property, null)
        };

        return value.ToString();
    }
        
    public override string ToString() 
        => $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.Offset}{Constant.Dote}{_property}";
}