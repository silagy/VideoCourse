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

    public static class Videos
    {
        public static string GetVideosByCreatorId => @"SELECT ""Id"", ""Url"", ""Name"", ""Description"", ""Duration"", ""CreatorId"", ""IsDeleted"", ""CreationDate"", ""UpdateDate""
        FROM public.""Videos""
        WHERE ""CreatorId"" = @CreatorId";
    }
    
}