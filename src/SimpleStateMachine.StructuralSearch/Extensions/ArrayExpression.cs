using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Extensions;

public static class ArrayExpression
{
    public static T[] Add<T>(this T[] array, params  T[] values)
    {
        var list = array.ToList();
        list.AddRange(values);
        return list.ToArray();
    }
}