using System;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderFileParameter : IPlaceholderRelatedRuleParameter
    {
        private readonly PlaceholderParameter _placeholderParameter;
        private readonly FileProperty _property;

        public PlaceholderFileParameter(PlaceholderParameter parameter, FileProperty property)
        {
            _placeholderParameter = parameter;
            _property = property;
        }
        
        public string Name => _placeholderParameter.Name;

        public string GetValue(ref IParsingContext context)
        {
            var placeHolder = _placeholderParameter.GetPlaceholder(ref context);
            var input = placeHolder.Input;
            
            return _property switch
            {
                FileProperty.Path => input.Path,
                FileProperty.Data => input.Data,
                FileProperty.Extension => input.Extension,
                FileProperty.Name => input.Name,
                FileProperty.Lenght => input.Lenght.ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(_property).FormatPrivateVar(), _property, null)
            };
        }
        
        public override string ToString() 
            => $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.File}{Constant.Dote}{_property}";
    }
}