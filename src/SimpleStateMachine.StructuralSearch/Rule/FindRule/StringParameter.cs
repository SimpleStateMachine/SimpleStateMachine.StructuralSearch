namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class StringParameter : IRuleParameter
    {
        public string Value { get; }
        public StringParameter(string value)
        {
            Value = value;
        }
        public string GetValue()
        {
            return Value;
        }
        
        public override string ToString()
        {
            var value = Value.Replace($"{Constant.DoubleQuotes}", $"{Constant.BackSlash}{Constant.DoubleQuotes}");
            return $"{Constant.DoubleQuotes}{value}{Constant.DoubleQuotes}";
        } 
    }
}