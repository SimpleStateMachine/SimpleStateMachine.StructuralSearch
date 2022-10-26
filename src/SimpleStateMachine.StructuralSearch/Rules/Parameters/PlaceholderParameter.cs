namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderParameter : IRuleParameter
    {
        public PlaceholderParameter(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
        
        public string GetValue(ref IParsingContext context)
        {
            return context.GetPlaceholder(Name).Value;
        }
        
        public IPlaceholder GetPlaceholder(ref IParsingContext context)
        {
            return context.GetPlaceholder(Name);
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
        }
    }
}