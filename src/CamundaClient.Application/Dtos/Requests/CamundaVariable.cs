using CamundaClient.Application.Utilities;
using Newtonsoft.Json;

namespace CamundaClient.Application.Dtos.Requests;

public record CamundaVariable
{
    public object Value { get; }
    public string? Type { get; }
    public Dictionary<string, object>? ValueInfo { get; }
    public bool? Local { get; }

    //[JsonConstructor]
    private CamundaVariable(object value, string? type = null, Dictionary<string, object>? valueInfo = null, bool? local = null)
    {
        Guard.NotNull(value, nameof(value));

        Value = value;
        Type = type;
        ValueInfo = valueInfo;
        Local = local;
    }


	/// <summary>
	/// Creates a new instance of the <see cref="CamundaVariable"/> class.
	/// </summary>
	/// <param name="value">The value of the variable. Cannot be null.</param>
	/// <param name="type">The type of the variable. Optional.</param>
	/// <param name="valueInfo">Additional information about the value. Optional.</param>
	/// <param name="local">Indicates if the variable is local. Nullable.</param>
	/// <returns>A new instance of the <see cref="CamundaVariable"/> class.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
	public static CamundaVariable Create(object value, string? type = null, Dictionary<string, object>? valueInfo = null, bool? local = null)
    {
        return new CamundaVariable(value, type, valueInfo, local);
    }

    ///// <summary>
    ///// Creates a variable with an optional local flag.
    ///// </summary>
    ///// <param name="value">The value of the variable. Cannot be null.</param>
    ///// <param name="type">The type of the variable (optional).</param>
    ///// <param name="valueInfo">Additional metadata about the variable (optional).</param>
    ///// <param name="local">Indicates if the variable is local. Nullable.</param>
    ///// <returns>A new instance of <see cref="CamundaVariable"/>.</returns>
    ///// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    //public static CamundaVariable CreateTriggerVariable(object value, string? type = null, Dictionary<string, object>? valueInfo = null, bool? local = null)
    //{
    //    return new CamundaVariable(value, type, valueInfo, local);
    //}
}

