using System;
using System.Globalization;
using System.Reflection;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

[CLSCompliant(false)]
public class StringMemberDataAttribute : MemberDataAttributeBase
{
    private readonly string _memberName;

    public StringMemberDataAttribute(string memberName, params object[] parameters)
        : base(memberName, parameters) { }

    protected override object[] ConvertDataItem(MethodInfo testMethod, object item)
    {
        if (item == null)
            return null;

        var str = item as string;
        if (str == null)
            throw new ArgumentException
            (
                string.Format(
                    CultureInfo.CurrentCulture,
                    "Property {0} on {1} yielded an item that is not string",
                    MemberName,
                    MemberType ?? testMethod.DeclaringType
                )
            );

        return [str];
    }
}