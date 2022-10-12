namespace VideoCourse.Infrastructure.Common.Queries;

public static class QueriesRepository
{

    public static class Users
    {
        public static string GetUserByEmail =>
            @"SELECT ""Id"", ""FirstName"", ""LastName"", ""Email"", ""Password"", ""RoleId"", ""CreationDate"", ""UpdateDate""
        FROM ""Users""
        WHERE ""IsDeleted"" = false AND ""Email"" = @Email";
    }
    
}