using System;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;

internal class IsSubRule : IFindRule
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
            PlaceholderType.Var => Grammar.Identifier.Before(CommonParser.Eof).TryParse(value, out _),
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