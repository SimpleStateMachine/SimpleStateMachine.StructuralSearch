using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderFileParameter : IRuleParameter
    {
        private readonly PlaceholderParameter _placeholderParameter;
        private readonly FileProperty _property;

        public PlaceholderFileParameter(PlaceholderParameter parameter, FileProperty property)
        {
            _placeholderParameter = parameter;
            _property = property;
        }

        public string GetValue()
        {
            var input = _placeholderParameter.GetPlaceholder().Input;
            return _property switch
            {
                FileProperty.Path => input.Path,
                FileProperty.Data => input.Data,
                FileProperty.Extension => input.Extension,
                FileProperty.Name => input.Name,
                FileProperty.Lenght => input.Lenght.ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{_placeholderParameter}{Constant.Dote}{PlaceholderProperty.File}{Constant.Dote}{_property}";
        }

        public void SetContext(ref IParsingContext context)
        {
            _placeholderParameter.SetContext(ref context);
        }
    }
}