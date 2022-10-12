using System.ComponentModel.DataAnnotations;
using ErrorOr;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static ErrorOr<Email> Create(string value)
    {
        var email = new EmailAddressAttribute();
        if (!email.IsValid(value))
        {
            return Error.Validation(
                code: "Email.IsNotValidEmail",
                description: $"The provided {value} is not a valid email");
        }

        return new Email(value);

    }

    public static implicit operator string(Email email) => email.Value;
}