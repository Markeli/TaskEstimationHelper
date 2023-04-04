using System.Runtime.CompilerServices;

namespace TasksEstimationHelper.Guards;

public static class Guard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AssertNotEmpty(this string value, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(fieldName, "Can't be null, empty or white space");
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AssertNotNull<T>(this T value, string fieldName)
    {
        if (value == null)
            throw new ArgumentNullException(fieldName, "Can't be null");
    }
}
