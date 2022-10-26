using System;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public class ChangeParameter : IRuleParameter
    {
        private readonly IRuleParameter _parameter;
        private readonly ChangeType _type;

        public ChangeParameter(IRuleParameter parameter, ChangeType type)
        {
            _parameter = parameter;
            _type = type;
        }

        public string GetValue(ref IParsingContext context)
        {
            var value = _parameter.GetValue(ref context);
            return _type switch
            {
                ChangeType.Trim => value.Trim(),
                ChangeType.TrimEnd => value.TrimEnd(),
                ChangeType.TrimStart => value.TrimStart(),
                ChangeType.ToUpper => value.ToUpper(),
                ChangeType.ToLower => value.ToLower(),
                _ => throw new ArgumentOutOfRangeException(nameof(_type).FormatPrivateVar(), _type, null)
            };
        }
        
        public override string ToString()
        {
            return $"{_parameter}{Constant.Dote}{_type}";
        }
    }
}