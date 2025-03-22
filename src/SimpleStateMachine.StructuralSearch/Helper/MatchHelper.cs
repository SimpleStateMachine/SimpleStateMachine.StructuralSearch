using System;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Helper;

internal static class MatchHelper
{
    public static ParenthesisType GetParenthesisType((char c1, char c2) parenthesis)
        => parenthesis switch
    {
        (Constant.LeftParenthesis, Constant.RightParenthesis) => ParenthesisType.Usual,
        (Constant.LeftSquareParenthesis, Constant.RightSquareParenthesis) => ParenthesisType.Square,
        (Constant.LeftCurlyParenthesis, Constant.RightCurlyParenthesis) => ParenthesisType.Curly,
        _ => throw new ArgumentOutOfRangeException(nameof(parenthesis), parenthesis, null)
    };
}