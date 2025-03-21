using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public class IsSubRule : IFindRule
    {
        private readonly PlaceholderType _argument;
        private readonly IRuleParameter _parameter;
        
        public IsSubRule(IRuleParameter parameter, PlaceholderType argument)
        {
            _parameter = parameter;
            _argument = argument;
        }

        public bool IsApplicableForPlaceholder(string placeholderName)
            => _parameter.IsApplicableForPlaceholder(placeholderName);

        public bool Execute(ref IParsingContext context)
        {
            var value = _parameter.GetValue(ref context);
            
            return _argument switch
            {
                PlaceholderType.Var => CommonParser.Identifier.Before(CommonParser.EOF).TryParse(value, out _),
                PlaceholderType.Int => int.TryParse(value, out _),
                PlaceholderType.Double => double.TryParse(value, out _),
                PlaceholderType.DateTime => DateTime.TryParse(value, out _),
                PlaceholderType.Guid => Guid.TryParse(value, out _),
                _ => throw new ArgumentOutOfRangeException(nameof(_argument).FormatPrivateVar(), _argument, null)
            };
        }
        
        public override string ToString() 
            => $"{_parameter}{Constant.Space}{SubRuleType.Is}{Constant.Space}{_argument}";
    }
}