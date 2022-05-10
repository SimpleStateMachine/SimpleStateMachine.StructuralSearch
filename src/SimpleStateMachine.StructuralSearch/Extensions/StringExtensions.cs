using System.IO;
using System.Text;

namespace SimpleStateMachine.StructuralSearch.Extensions;

public static class StringExtensions
{
    public static MemoryStream AsStream(this string str)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(str));
    }
}