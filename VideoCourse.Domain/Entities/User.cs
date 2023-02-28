using System.Net.Sockets;
using ErrorOr;
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

    private List<Role> _roles { get; set; } = new();

    public IReadOnlyCollection<Role> Roles => _roles;

    protected User()
    {
    }

    public User(
        Guid id,
        string firstName,
        string lastName,
        Email email,
        string password
    ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        
        RaiseDomainEvent(new UserCreatedDomainEvent(id));
    }

    public ErrorOr<bool> AddRoles(IEnumerable<Role> roles)
    {
        _roles.Clear();
        _roles.AddRange(roles);
        
        return true;
    }
}