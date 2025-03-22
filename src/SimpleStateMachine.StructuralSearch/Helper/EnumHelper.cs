using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Helper;

internal static class EnumHelper
{
    public static bool IsEnumType(Type? enumType) 
        => enumType is { IsEnum: true };

    public static bool Contains<TEnum>(Type enumType, string value, bool exception = true)
        where TEnum : struct, Enum
    {
        if (string.IsNullOrEmpty(value))
            return false;
            
        if (!exception && !IsEnumType(enumType))
            return false;
            
        return Enum.IsDefined(enumType, value);
    }

    public static string? GetName<TEnum>(TEnum value) 
        where TEnum : struct, Enum 
        => Enum.GetName(value);

    public static string? Name(this Enum enumType) 
        => Enum.GetName(enumType.GetType(), enumType);

    public static IEnumerable<string>? GetNames<TEnum>()
        where TEnum : struct, Enum 
        => Enum.GetNames<TEnum>();

    public static IEnumerable<string> GetNamesExcept<TEnum>(params TEnum [] excludedElements)
        where TEnum : struct, Enum 
        => GetValueExcept(excludedElements).Select(x=> x.Name()).OfType<string>();

    public static IEnumerable<TEnum> GetValues<TEnum>() 
        where TEnum : struct, Enum 
        => Enum.GetValues<TEnum>();

    public static IEnumerable<TEnum> GetValueExcept<TEnum>(params TEnum [] excludedElements) 
        where TEnum : struct, Enum 
        => GetValues<TEnum>().Where(x => !excludedElements.Contains(x));
        
    public static TEnum GetValue<TEnum>(string value, bool ignoreCase = true)
        where TEnum : struct, Enum
    {
        TryGetValue(value, ignoreCase, out TEnum result);
        return result;
    }

    public static bool TryGetValue<TEnum>(string value, bool ignoreCase, out TEnum result) 
        where TEnum : struct, Enum 
        => Enum.TryParse(value, ignoreCase, out result);
}