using System.Text.RegularExpressions;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Operator.Logical;

internal class MatchOperation : ILogicalOperation
{
    private readonly IParameter _stringParameter;
    private readonly string _regex;

    public MatchOperation(IParameter stringParameter, string regex)
    {
        _stringParameter = stringParameter;
        _regex = regex;
    }

    public bool Execute(ref IParsingContext context)
    {
        var value = _stringParameter.GetValue(ref context);
        return Regex.IsMatch(input: value, pattern: _regex);
    }

    public override string ToString()
        => $"{_stringParameter}{Constant.Space}{Constant.Match}{Constant.Space}{_regex}";
}