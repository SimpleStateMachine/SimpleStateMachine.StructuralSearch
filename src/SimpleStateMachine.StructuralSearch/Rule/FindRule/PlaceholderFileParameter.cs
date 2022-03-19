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
            throw new System.NotImplementedException();
        }
    }
}