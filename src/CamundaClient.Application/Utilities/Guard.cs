namespace CamundaClient.Application.Utilities;

internal static class Guard
{
    /// <summary>
    /// Ensures that the specified reference type parameter is not null.
    /// </summary>
    /// <typeparam name="T">The type of the parameter value.</typeparam>
    /// <param name="parameterValue">The parameter value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The parameter value if it is not null.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the parameter value is null.</exception>
    internal static T NotNull<T>(T? parameterValue, string parameterName) where T : class
    {
        if (parameterValue == null)
        {
            throw new ArgumentNullException(parameterName, $"{parameterName} must not be null.");
        }

        return parameterValue;
    }

    /// <summary>
    /// Ensures that the specified reference type parameter is not null, with a custom error message.
    /// </summary>
    /// <typeparam name="T">The type of the parameter value.</typeparam>
    /// <param name="parameterValue">The parameter value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>The parameter value if it is not null.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the parameter value is null.</exception>
    internal static T NotNull<T>(T? parameterValue, string parameterName, string message) where T : class
    {
        if (parameterValue == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        return parameterValue;
    }

    /// <summary>
    /// Ensures that the specified nullable value type parameter has a value.
    /// </summary>
    /// <typeparam name="T">The value type of the parameter.</typeparam>
    /// <param name="parameterValue">The nullable parameter value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The value of the parameter if it has a value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the parameter does not have a value.</exception>
    internal static T NotNull<T>(T? parameterValue, string parameterName) where T : struct
    {
        if (!parameterValue.HasValue)
        {
            throw new ArgumentNullException(parameterName, $"{parameterName} must have a value.");
        }

        return parameterValue.Value;
    }

    /// <summary>
    /// Ensures that the specified nullable value type parameter has a value, with a custom error message.
    /// </summary>
    /// <typeparam name="T">The value type of the parameter.</typeparam>
    /// <param name="parameterValue">The nullable parameter value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>The value of the parameter if it has a value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the parameter does not have a value.</exception>
    internal static T NotNull<T>(T? parameterValue, string parameterName, string message) where T : struct
    {
        if (!parameterValue.HasValue)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        return parameterValue.Value;
    }

    /// <summary>
    /// Ensures that the specified string is not null or empty.
    /// </summary>
    /// <param name="value">The string value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The string value if it is not null or empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the string is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the string is empty.</exception>
    internal static string NotNullOrEmpty(string value, string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, $"{parameterName} must not be null.");
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException($"{parameterName} must not be empty.", parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures that the specified string is not null or empty, with a custom error message.
    /// </summary>
    /// <param name="value">The string value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>The string value if it is not null or empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the string is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the string is empty.</exception>
    internal static string NotNullOrEmpty(string value, string parameterName, string message)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException(message, parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures that the specified string is not null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The string value if it is not null, empty, or white-space.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the string is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the string is empty or white-space.</exception>
    internal static string NotNullOrWhiteSpace(string value, string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, $"{parameterName} must not be null.");
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{parameterName} must not be empty or white-space.", parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures that the specified string is not null, empty, or consists only of white-space characters, with a custom error message.
    /// </summary>
    /// <param name="value">The string value to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>The string value if it is not null, empty, or white-space.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the string is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the string is empty or white-space.</exception>
    internal static string NotNullOrWhiteSpace(string value, string parameterName, string message)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures that the specified collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The collection if it is not null or empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the collection is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    internal static IEnumerable<T> NotNullOrEmpty<T>(IEnumerable<T> collection, string parameterName)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(parameterName, $"{parameterName} must not be null.");
        }

        if (!collection.Any())
        {
            throw new ArgumentException($"{parameterName} must not be empty.", parameterName);
        }

        return collection;
    }

    /// <summary>
    /// Ensures that the specified collection is not null or empty, with a custom error message.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>The collection if it is not null or empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the collection is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    internal static IEnumerable<T> NotNullOrEmpty<T>(IEnumerable<T> collection, string parameterName, string message)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(parameterName, message);
        }

        if (!collection.Any())
        {
            throw new ArgumentException(message, parameterName);
        }

        return collection;
    }

    /// <summary>
    /// Ensures that the specified integer value is greater than or equal to the minimum value.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="minValue">The minimum allowed value.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The integer value if it meets the condition.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than the minimum value.</exception>
    internal static int GreaterThanOrEqual(int value, int minValue, string parameterName)
    {
        if (value < minValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, value, $"{parameterName} must be greater than or equal to {minValue}.");
        }

        return value;
    }

    /// <summary>
    /// Ensures that the specified integer value is greater than or equal to the minimum value, with a custom error message.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="minValue">The minimum allowed value.</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>The integer value if it meets the condition.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than the minimum value.</exception>
    internal static int GreaterThanOrEqual(int value, int minValue, string parameterName, string message)
    {
        if (value < minValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, value, message);
        }

        return value;
    }

    /// <summary>
    /// Ensures that the specified value is within the specified range.
    /// </summary>
    /// <typeparam name="T">The type of the value, which must implement <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="minValue">The minimum allowed value (inclusive).</param>
    /// <param name="maxValue">The maximum allowed value (inclusive).</param>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <returns>The value if it is within the specified range.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is outside the specified range.</exception>
    internal static T InRange<T>(T value, T minValue, T maxValue, string parameterName) where T : IComparable<T>
    {
        if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, value, $"{parameterName} must be between {minValue} and {maxValue}.");
        }

        return value;
    }

    /// <summary>
    /// Ensures that the specified condition is true; otherwise, throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="parameterName">The name of the parameter being evaluated.</param>
    /// <param name="message">The error message to include if the condition is false.</param>
    /// <exception cref="ArgumentException">Thrown when the condition is false.</exception>
    internal static void IsTrue(bool condition, string parameterName, string message)
    {
        if (!condition)
        {
            throw new ArgumentException(message, parameterName);
        }
    }

    /// <summary>
    /// Ensures that the specified condition is false; otherwise, throws an <see cref="ArgumentException"/>.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="parameterName">The name of the parameter being evaluated.</param>
    /// <param name="message">The error message to include if the condition is true.</param>
    /// <exception cref="ArgumentException">Thrown when the condition is true.</exception>
    internal static void IsFalse(bool condition, string parameterName, string message)
    {
        if (condition)
        {
            throw new ArgumentException(message, parameterName);
        }
    }
}
