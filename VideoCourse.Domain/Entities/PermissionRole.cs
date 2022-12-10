namespace VideoCourse.Domain.Entities;

public class PermissionRole
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public PermissionRole()
    {
        
    }

    public PermissionRole(int roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}