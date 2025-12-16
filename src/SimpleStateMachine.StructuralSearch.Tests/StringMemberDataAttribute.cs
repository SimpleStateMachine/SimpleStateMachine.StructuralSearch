using System;
using System.Globalization;
using System.Reflection;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class StringMemberDataAttribute(string memberName, params object[] parameters)
    : MemberDataAttributeBase(memberName, parameters)
{
    protected override object[] ConvertDataItem(MethodInfo testMethod, object item)
    {
        if (item is not string str)
            throw new ArgumentException
            (
                string.Format
                (
                    CultureInfo.CurrentCulture,
                    "Property {0} on {1} yielded an item that is not string",
                    MemberName,
                    MemberType ?? testMethod.DeclaringType
                )
            );

        return [str];
    }
}