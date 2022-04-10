using System;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRule
{
    public class ChangeParameter : IRuleParameter
    {
        public IRuleParameter Parameter { get; }
        public ChangeType Type { get; }

        public ChangeParameter(IRuleParameter parameter, ChangeType type)
        {
            Parameter = parameter;
            Type = type;
        }

        public string GetValue()
        {
            var value = Parameter.GetValue();
            return Type switch
            {
                ChangeType.Trim => value.Trim(),
                ChangeType.TrimEnd => value.TrimEnd(),
                ChangeType.TrimStart => value.TrimStart(),
                ChangeType.ToUpper => value.ToUpper(),
                ChangeType.ToLower => value.ToLower(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public override string ToString()
        {
            return $"{Parameter}{Constant.Dote}{Type}";
        } 
    }
}