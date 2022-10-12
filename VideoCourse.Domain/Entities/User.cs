using VideoCourse.Domain.Enums;
using VideoCourse.Domain.ValueObjects;

namespace VideoCourse.Domain.Entities;

public class User : AggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
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
        UserRole role,
        DateTime creationDate,
        DateTime updateDate
    ) : base(id, creationDate, updateDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        RoleId = (int)role;
    }
}