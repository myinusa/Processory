namespace Processory.Utilities;
public static class NullSafe {
    /// <summary>
    /// Safely dereferences a possibly null reference, returning a default value if the reference is null.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The nullable value to dereference.</param>
    /// <param name="defaultValue">The default value to return if the nullable value is null.</param>
    /// <returns>The value if it is not null; otherwise, the default value.</returns>
    public static T GetValueOrDefault<T>(T? value, T defaultValue)
        where T : struct
    {
        return value ?? defaultValue;
    }

    /// <summary>
    /// Safely dereferences a possibly null reference type, returning a default value if the reference is null.
    /// </summary>
    /// <typeparam name="T">The type of the reference.</typeparam>
    /// <param name="value">The nullable reference to dereference.</param>
    /// <param name="defaultValue">The default value to return if the reference is null.</param>
    /// <returns>The value if it is not null; otherwise, the default value.</returns>
    public static T GetValueOrDefault<T>(T? value, T defaultValue)
        where T : class
    {
        return value ?? defaultValue;
    }
}
