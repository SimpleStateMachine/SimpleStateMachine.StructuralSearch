using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;

internal class InSubRule : IFindRule
{
    private readonly IRuleParameter _parameter;

    private readonly IEnumerable<IRuleParameter> _arguments;
        
    public InSubRule(IRuleParameter parameter, IEnumerable<IRuleParameter> arguments)
    {
        _parameter = parameter;
            
        _arguments = arguments;
    }

    public bool Execute(ref IParsingContext context)
    {
        var value = _parameter.GetValue(ref context);

        foreach (var argument in _arguments)
        {
            var valueForResult = argument.GetValue(ref context);
                
            if (Equals(value, valueForResult))
                return true;
        }
            
        return false;
    }
        
    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameter.IsApplicableForPlaceholder(placeholderName) || _arguments.Any(p => p.IsApplicableForPlaceholder(placeholderName));
        
    public override string ToString() 
        => $"{_parameter}{Constant.Space}{SubRuleType.In}{Constant.Space}{string.Join(Constant.Comma, _arguments.Select(x=>x.ToString()))}";
}