using System;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public class ChangeUnaryParameter : IRuleParameter
{
    public IRuleParameter Parameter { get; }
    public ChangeUnaryType Type { get; }
    public IRuleParameter Arg { get; }
    
    public ChangeUnaryParameter(IRuleParameter parameter, ChangeUnaryType type, IRuleParameter arg)
    {
        Parameter = parameter;
        Type = type;
        Arg = arg;
    }

    public string GetValue()
    {
        var parameter = Parameter.GetValue();
        var arg = Arg.GetValue();
        return Type switch
        {
            ChangeUnaryType.RemoveSubStr => parameter.Replace(arg, string.Empty),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
        
    public override string ToString()
    {
        return $"{Parameter}{Constant.Dote}{Type}{Constant.LeftParenthesis}{Arg}{Constant.RightParenthesis}";
    }

    public void SetContext(ref IParsingContext context)
    {
        Parameter.SetContext(ref context);
        Arg.SetContext(ref context);
    }
}