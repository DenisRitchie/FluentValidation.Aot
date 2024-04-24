namespace FluentValidation;

using System;
using System.Collections.Generic;

/// <summary>
///     Defines a validation failure.
/// </summary>
/// <param name="PropertyName">
///     The name of the property.
/// </param>
/// <param name="ErrorMessage">
///     The error message.
/// </param>
/// <param name="AttemptedValue">
///     The property value that caused the failure.
/// </param>
[Serializable]
public record ValidationFailure(string? PropertyName = null, string? ErrorMessage = null, object? AttemptedValue = null)
{
    /// <summary>
    ///     Custom state associated with the failure.
    /// </summary>
    public object? CustomState { get; set; }


    /// <summary>
    ///     Custom severity level associated with the failure.
    /// </summary>
    public Severity Severity { get; set; } = Severity.Error;


    /// <summary>
	///     Gets or sets the error code.
	/// </summary>
	public string? ErrorCode { get; set; }


    /// <summary>
    ///     Gets or sets the formatted message placeholder values.
    /// </summary>
    public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; } = [];

    /// <summary>
    ///     Creates a textual representation of the failure.
    /// </summary>
    public override string ToString() => ErrorMessage ?? string.Empty;
}
