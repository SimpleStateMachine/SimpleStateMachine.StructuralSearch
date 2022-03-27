namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class PlaceholderParameter : IRuleParameter
    {
        public string Name { get; }
        
        public PlaceholderParameter(string name)
        {
            Name = name;
        }
        
        public string GetValue()
        {
            throw new System.NotImplementedException();
        }
        
        public override string ToString()
        {
            return $"{Constant.PlaceholderSeparator}{Name}{Constant.PlaceholderSeparator}";
        } 
    }
}