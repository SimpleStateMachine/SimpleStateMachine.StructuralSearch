using System;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Rules;

public class UnaryRule : IFindRule
{
    private readonly UnaryRuleType _type;
    private readonly IFindRule _parameter;

    public UnaryRule(UnaryRuleType type, IFindRule parameter)
    {
        _type = type;
        _parameter = parameter;
    }

    public bool Execute(ref IParsingContext context)
    {
        var result = _parameter.Execute(ref context);
            
        return _type switch
        {
            UnaryRuleType.Not => !result,
            _ => throw new ArgumentOutOfRangeException(nameof(_type).FormatPrivateVar(), _type, null)
        };
    }

    public bool IsApplicableForPlaceholder(string placeholderName)
        => _parameter.IsApplicableForPlaceholder(placeholderName);
        
    public override string ToString() 
        => $"{_type}{Constant.Space}{_parameter}";
}