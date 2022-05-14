using SimpleStateMachine.StructuralSearch.Helper;

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
            var value = EscapeHelper.EscapeChars(Value, c => $"{Constant.BackSlash}{c}", Constant.Parameter.Escape);
            
            return $"{value}";
        } 
    }
}