using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class StringParameter : IRuleParameter
    {
        private readonly string _value;
        
        public StringParameter(string value)
        {
            _value = value;
        }
        public string GetValue()
        {
            return _value;
        }
        
        public override string ToString()
        {
            var value = EscapeHelper.EscapeChars(_value, c => $"{Constant.BackSlash}{c}", Constant.Parameter.Escape);
            
            return $"{value}";
        }

        public void SetContext(IParsingContext context)
        {
            
        }
    }
}