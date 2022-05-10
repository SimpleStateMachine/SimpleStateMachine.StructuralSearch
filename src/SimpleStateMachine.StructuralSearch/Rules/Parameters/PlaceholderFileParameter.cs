using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderFileParameter : IRuleParameter
    {
        public PlaceholderParameter PlaceholderParameter { get; }
        public FileProperty Property { get; }

        public PlaceholderFileParameter(PlaceholderParameter parameter, FileProperty property)
        {
            PlaceholderParameter = parameter;
            Property = property;
        }

        public string GetValue()
        {
            var input = PlaceholderParameter.GetPlaceholder().Input;
            return Property switch
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
            return $"{PlaceholderParameter}{Constant.Dote}{PlaceholderProperty.File}{Constant.Dote}{Property}";
        } 
    }
}