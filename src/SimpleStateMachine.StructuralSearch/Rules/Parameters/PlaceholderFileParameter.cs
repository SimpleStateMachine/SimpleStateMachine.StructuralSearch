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
            var file = PlaceholderParameter.GetPlaceholder().File;
            return Property switch
            {
                FileProperty.Path => file.Path,
                FileProperty.Data => file.Data,
                FileProperty.Name => file.Name,
                FileProperty.Directory => file.Directory,
                FileProperty.Lenght => file.Lenght.ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{PlaceholderParameter}{Constant.Dote}{PlaceholderProperty.File}{Constant.Dote}{Property}";
        } 
    }
}