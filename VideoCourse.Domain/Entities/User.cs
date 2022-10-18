using VideoCourse.Domain.Enums;
using VideoCourse.Domain.Events;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class User : AggregateRoot
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public int RoleId { get; private set; }
    public UserRole Role => (UserRole)RoleId;

    protected User()
    {
    }

    public User(
        Guid id,
        string firstName,
        string lastName,
        Email email,
        string password,
        UserRole role
    ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        RoleId = (int)role;
        
        RaiseDomainEvent(new UserCreatedDomainEvent(id));
    }
}