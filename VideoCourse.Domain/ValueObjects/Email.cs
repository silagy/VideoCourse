using System.ComponentModel.DataAnnotations;
using ErrorOr;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Primitives;
using VideoCourse.Domain.Shared;

namespace VideoCourse.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    private protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static ErrorOr<Email> Create(string value)
    {
        var email = new EmailAddressAttribute();
        return ErrorOr.ErrorOr.From(value)
            .Ensure(e => !string.IsNullOrEmpty(e),
                CustomErrors.Entity.EntityNotNull)
            .Ensure(e => email.IsValid(e),
            CustomErrors.Email.EmailNotValid(value))
            .Map(e => new Email(e));
    }

    public static implicit operator string(Email email) => email.Value;
}