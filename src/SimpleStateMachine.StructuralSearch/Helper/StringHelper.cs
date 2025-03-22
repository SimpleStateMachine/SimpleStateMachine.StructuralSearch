using System.Text;

namespace SimpleStateMachine.StructuralSearch.Helper;

internal static class StringHelper
{
    public static string FormatPrivateVar(this string str)
    {
        var stringBuilder = new StringBuilder(str.Length - 1);
        stringBuilder.Append(char.ToUpper(str[1]));
        stringBuilder.Append(str[2..]);
        return stringBuilder.ToString();
    }
}