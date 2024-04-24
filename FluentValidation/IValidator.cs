namespace FluentValidation;

using System;
using System.Threading;
using System.Threading.Tasks;

using Results;

/// <summary>
///     Defines a validator for a particular type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IValidator<in T> : IValidator
{
    /// <summary>
    ///     Validates the specified instance.
    /// </summary>
    /// <param name="Instance">
    ///     The instance to validate.
    /// </param>
    /// <returns>
    ///     A ValidationResult object containing any validation failures.
    /// </returns>
    ValidationResult Validate(T Instance);

    /// <summary>
    ///     Validate the specified instance asynchronously.
    /// </summary>
    /// <param name="Instance">
    ///     The instance to validate.
    /// </param>
    /// <param name="Cancellation"></param>
    /// <returns>
    ///     A ValidationResult object containing any validation failures.
    /// </returns>
    ValueTask<ValidationResult> ValidateAsync(T Instance, CancellationToken Cancellation = default);
}

/// <summary>
///     Defines a validator for a particular type.
/// </summary>
public interface IValidator
{
    /// <summary>
    ///     Validates the specified instance.
    /// </summary>
    /// <param name="Context">
    ///     A ValidationContext.
    /// </param>
    /// <returns>
    ///     A ValidationResult object contains any validation failures.
    /// </returns>
    ValidationResult Validate(IValidationContext Context);

    /// <summary>
    ///     Validates the specified instance asynchronously.
    /// </summary>
    /// <param name="Context">
    ///     A ValidationContext.
    /// </param>
    /// <param name="Cancellation">
    ///     Cancellation token.
    /// </param>
    /// <returns>
    ///     A ValidationResult object contains any validation failures.
    /// </returns>
    ValueTask<ValidationResult> ValidateAsync(IValidationContext Context, CancellationToken Cancellation = default);

    /// <summary>
    ///     Creates a hook to access various meta data properties.
    /// </summary>
    /// <returns>
    ///     A IValidatorDescriptor object which contains methods to access metadata.
    /// </returns>
    IValidatorDescriptor CreateDescriptor();
}
