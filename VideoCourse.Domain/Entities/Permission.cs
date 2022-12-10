using System.Runtime.CompilerServices;
using ErrorOr;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Domain.Entities;

public sealed class Permission
{
    public int Id { get; init; }
    public string Name { get; init; }
    public Permissions PermissionBit => (Permissions)Id;

    private Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ErrorOr<Permission> Create(Permissions permission)
    {
        var perm = new Permission((int)permission, permission.ToString());
        return perm;
    }
}