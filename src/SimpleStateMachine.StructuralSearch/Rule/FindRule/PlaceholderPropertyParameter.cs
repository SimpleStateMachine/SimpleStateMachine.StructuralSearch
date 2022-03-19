using System;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderPropertyParameter: IRuleParameter
    {
        public string Name { get; }
        
        public PlaceholderProperty Property { get; }

        public PlaceholderPropertyParameter(string name, PlaceholderProperty property)
        {
            Name = name;
            Property = property;
        }
        
        public string GetValue()
        {
            //TODO work with placeholder meta
            return Property switch
            {
                PlaceholderProperty.Lenght => "10",
                PlaceholderProperty.File => "11",
                PlaceholderProperty.Position => "12",
                PlaceholderProperty.Line => "13",
                PlaceholderProperty.Column => "14",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}