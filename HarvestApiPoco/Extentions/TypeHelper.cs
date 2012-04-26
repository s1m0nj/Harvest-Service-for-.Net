using System;

public static class TypeHelper
{

    public static bool IsNullableType(this Type theType)
    {
        return (theType.IsGenericType &&
        theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
    }
}