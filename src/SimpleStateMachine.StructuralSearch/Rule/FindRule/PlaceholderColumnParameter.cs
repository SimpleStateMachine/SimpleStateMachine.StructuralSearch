namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderColumnParameter : IRuleParameter
    {
        public PlaceholderParameter PlaceholderParameter { get; }
        public ColumnProperty Property { get; }

        public PlaceholderColumnParameter(PlaceholderParameter parameter, ColumnProperty property)
        {
            PlaceholderParameter = parameter;
            Property = property;
        }
        
        public string GetValue()
        {
            throw new System.NotImplementedException();
        }
        
        public override string ToString()
        {
            return $"{PlaceholderParameter}.{PlaceholderProperty.Column}.{Property}";
        } 
    }
}