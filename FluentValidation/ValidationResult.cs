namespace FluentValidation.Results;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     The result of running a validator.
/// </summary>
[Serializable]
public class ValidationResult
{
    private List<ValidationFailure> _Errors;

    /// <summary>
    ///     Whether validation succeeded.
    /// </summary>
    public virtual bool IsValid => Errors.Count == 0;

    /// <summary>
    ///     A collection of errors.
    /// </summary>
    public List<ValidationFailure> Errors
    {
        get => _Errors;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            // Ensure any nulls are removed and the list is copied to be consistent with the constructor below.
            _Errors = value.Where(Failure => Failure is not null).ToList();
        }
    }

    /// <summary>
    ///     The RuleSets that were executed during the validation run.
    /// </summary>
    public string[]? RuleSetsExecuted { get; set; }

    /// <summary>
    /// Creates a new ValidationResult
    /// </summary>
    public ValidationResult()
    {
        _Errors = [];
    }

    /// <summary>
    ///     Creates a new ValidationResult from a collection of failures.
    /// </summary>
    /// <param name="Failures">
    ///     Collection of <see cref="ValidationFailure"/> instances which is later available through the <see cref="Errors"/> property.
    /// </param>
    /// <remarks>
    ///     Any nulls will be excluded.
    ///     The list is copied.
    /// </remarks>
    public ValidationResult(IEnumerable<ValidationFailure> Failures)
    {
        _Errors = Failures.Where(Failure => Failure != null).ToList();
    }

    /// <summary>
    ///     Creates a new ValidationResult by combining several other ValidationResults.
    /// </summary>
    /// <param name="OtherResults"></param>
    public ValidationResult(IEnumerable<ValidationResult> OtherResults)
    {
        _Errors = OtherResults.SelectMany(Result => Result.Errors).ToList();
        RuleSetsExecuted = OtherResults.Where(Result => Result.RuleSetsExecuted is not null)
                                       .SelectMany(Result => Result.RuleSetsExecuted)
                                       .Distinct()
                                       .ToArray();
    }

    internal ValidationResult(List<ValidationFailure> Errors)
    {
        _Errors = Errors;
    }

    /// <summary>
    ///     Generates a string representation of the error messages separated by new lines.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return ToString(Environment.NewLine);
    }

    /// <summary>
    ///     Generates a string representation of the error messages separated by the specified character.
    /// </summary>
    /// <param name="Separator">
    ///     The character to separate the error messages.
    /// </param>
    /// <returns></returns>
    public string ToString(string Separator)
    {
        return string.Join(Separator, _Errors.Select(Failure => Failure.ErrorMessage));
    }

    /// <summary>
    ///     Converts the ValidationResult's errors collection into a simple dictionary representation.
    /// </summary>
    /// <returns>
    ///     A dictionary keyed by property name where each value is an array of error messages associated with that property.
    /// </returns>
    public IDictionary<string, string[]> ToDictionary()
    {
        return Errors.GroupBy(Error => Error.PropertyName)
                     .ToDictionary(Group => Group.Key!,
                                   Group => Group.Select(Error => Error.ErrorMessage!).ToArray());
    }
}
