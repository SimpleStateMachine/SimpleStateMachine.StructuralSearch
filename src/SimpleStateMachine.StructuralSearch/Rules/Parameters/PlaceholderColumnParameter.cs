using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal class PlaceholderColumnParameter : IPlaceholderPropertyRuleParameter
{
    private readonly ColumnProperty _property;

    public PlaceholderColumnParameter(PlaceholderParameter parameter, ColumnProperty property)
    {
        Placeholder = parameter;
        _property = property;
    }
        
    public PlaceholderParameter Placeholder { get; }
        
    public string GetValue(ref IParsingContext context)
    {
        var placeHolder = Placeholder.GetPlaceholder(ref context);
        var column = placeHolder.Column;
            
        var value = _property switch
        {
            ColumnProperty.Start => column.Start,
            ColumnProperty.End => column.End,
            _ => throw new ArgumentOutOfRangeException(nameof(_property).FormatPrivateVar(), _property, null)
        };

        return value.ToString();
    }
        
    public override string ToString() 
        => $"{Placeholder}{Constant.Dote}{PlaceholderProperty.Column}{Constant.Dote}{_property}";
}