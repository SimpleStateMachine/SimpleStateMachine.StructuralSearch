using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderLenghtParameter: IRuleParameter
    {
        public PlaceholderParameter PlaceholderParameter { get; }
        public PlaceholderProperty Property { get; }

        public PlaceholderLenghtParameter(PlaceholderParameter parameter, PlaceholderProperty property)
        {
            PlaceholderParameter = parameter;
            Property = property;
        }
        
        public string GetValue()
        {
            return PlaceholderParameter.GetPlaceholder().Lenght.ToString();
        }
        
        public override string ToString()
        {
            return $"{PlaceholderParameter}{Constant.Dote}{Property}";
        } 
    }
}