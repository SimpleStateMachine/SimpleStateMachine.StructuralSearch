using System;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderColumnParameter : IPlaceholderRelatedRuleParameter
    {
        private readonly PlaceholderParameter _placeholderParameter;
        private readonly ColumnProperty _property;

        public PlaceholderColumnParameter(PlaceholderParameter parameter, ColumnProperty property)
        {
            _placeholderParameter = parameter;
            _property = property;
        }
        
        public string Name => _placeholderParameter.Name;
        
        public string GetValue(ref IParsingContext context)
        {
            var placeHolder = _placeholderParameter.GetPlaceholder(ref context);
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
            => $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.Column}{Constant.Dote}{_property}";
    }
}