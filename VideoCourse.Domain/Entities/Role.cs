using ErrorOr;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Enums;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Domain.Entities;

public sealed class Role
{
    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    private List<Permission> _permissions { get; set; } = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;

    public ICollection<User> Users { get; set; }

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Role() { }

    public ErrorOr<Permission> AddPermission(Permissions permission)
    {
        if (_permissions.Any(p => p.PermissionBit == permission))
            return CustomErrors.Permission.PermissionAlreadyExists(permission);

        Permission perm = Permission.Create(permission).Value;
        _permissions.Add(perm);

        return perm;
    }

    public void AddPermissions(List<Permissions> permissions)
    {
        foreach (var permission in permissions)
        {
            if(_permissions.Any(p => p.PermissionBit == permission)) continue;
            _permissions.Add(Permission.Create(permission).Value);
        }
    }
}