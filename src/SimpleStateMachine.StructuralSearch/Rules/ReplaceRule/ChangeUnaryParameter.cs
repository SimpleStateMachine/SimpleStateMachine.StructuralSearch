using System;
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

    public string GetValue()
    {
        var parameter = _parameter.GetValue();
        var arg = _arg.GetValue();
        return _type switch
        {
            ChangeUnaryType.RemoveSubStr => parameter.Replace(arg, string.Empty),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
        
    public override string ToString()
    {
        return $"{_parameter}{Constant.Dote}{_type}{Constant.LeftParenthesis}{_arg}{Constant.RightParenthesis}";
    }

    public void SetContext(IParsingContext context)
    {
        _parameter.SetContext(context);
        _arg.SetContext(context);
    }
}