using System;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public class ChangeUnaryParameter : IRuleParameter
{
    private readonly IRuleParameter _parameter;
    private readonly ChangeUnaryType _type;
    private readonly IRuleParameter _arg;
    
    public ChangeUnaryParameter(IRuleParameter parameter, ChangeUnaryType type, IRuleParameter arg)
    {
        _parameter = parameter;
        _type = type;
        _arg = arg;
    }

    public string GetValue(ref IParsingContext context)
    {
        var parameter = _parameter.GetValue(ref context);
        var arg = _arg.GetValue(ref context);
        return _type switch
        {
            ChangeUnaryType.RemoveSubStr => parameter.Replace(arg, string.Empty),
            _ => throw new ArgumentOutOfRangeException(nameof(_type).FormatPrivateVar(), _type, null)
        };
    }
        
    public override string ToString() 
        => $"{_parameter}{Constant.Dote}{_type}{Constant.LeftParenthesis}{_arg}{Constant.RightParenthesis}";
}