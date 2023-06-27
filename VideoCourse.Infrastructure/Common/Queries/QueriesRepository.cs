namespace VideoCourse.Infrastructure.Common.Queries;

public static class QueriesRepository
{

    public static class Users
    {
        public static string GetUserByEmail =>
            @"select
                id as Id,
                first_name as FirstName,
                last_name as LastName,
                email as Email,
                password as Password,
                role_id as RoleId,
                creation_date as CreationDate,
                update_date as UpdateDate
            from users
            where email = @email
            and is_deleted = false";

        public static string GetUsersByRoleId =>
            @"select id as Id,
       first_name as FirstName,
       last_name as LastName,
       email as Email,
    is_deleted as IsDeleted,
    creation_date as CreationDate,
    update_date as UpdateDate from users
    where is_deleted = false
    order by creation_date
    limit @PageSize
    offset @Page";
        
        public static string GetCreators => $@"select
    id as Id,
       first_name as FirstName,
       last_name as LastName,
       email as Email,
        role_id as RoleId,
        is_deleted as IsDeleted,
        creation_date as CreationDate,
        update_date as UpdateDate
from users as us
inner join user_roles ur on us.id = ur.user_id
where us.is_deleted = false
and ur.role_id = 1
and us.id in(select distinct creator_id from videos)";
    }

    public static class Roles
    {
        public static string GetAllRoles => $"select id as Id, name as Name from roles";

        public static string GetRolesByIds => $"select id as Id, name as Name from roles where id = ANY(@Ids)";
    }

   

    public static class Videos
    {
        public static string GetVideosByCreatorId => @"select
    id as Id,
    url as Url,
    name as Name,
    duration as Duration,
    description as Description,
    creator_id as CreatorId,
    is_deleted as IsDeleted,
    creation_date as CreationDate,
    update_date as UpdateDate,
    is_published as IsPublished,
    published_on_utc as PublishedOnUtc
from videos
where creator_id = @CreatorId";

        public static string GetVideoWithAllItemsMultipleQuery =>
            @"select 
    id as Id,
    url as Url,
    name as Name,
    is_deleted as IsDeleted,
    description as Description,
    duration as Duration,
    creator_id as CreatorId,
    is_published as IsPublished,
    published_on_utc as PublishedOnUtc,
    creation_date as CreationDate,
    update_date as UpdateDate from videos where id = @VideoId and videos.is_deleted = false;
select 
    id as Id,
    name as Name,
    description as Description,
    start_time as StartTime,
    end_time as EndTime,
    video_id as VideoId,
    is_deleted as IsDeleted,
    creation_date as CreationDate,
    update_date as UpdateDate from sections where video_id = @VideoId and sections.is_deleted = false;
select 
    id as Id,
    is_deleted as IsDeleted,
    creation_date as CreationDate,
    update_date as UpdateDate,
    name as Name,
    content as Content,
    time as Time,
    type_id as TypeId,
    video_id as VideoId
from notes where video_id = @VideoId and notes.is_deleted = false;
select 
    id as Id,
    feedback as Feedback,
    is_deleted as IsDeleted,
    creation_date as CreationDate,
    update_date as UpdateDate,
    name as Name,
    content as Content,
    time as Time,
    type_id as TypeId,
    video_id as VideoId
from questions where video_id = @VideoId and questions.is_deleted = false;";
        
        public static string GetVideosWithParameters => $@"select
                                                        id as Id,
                                                        url as Url,
                                                        name as Name,
                                                        duration as Duration,
                                                        description as Description,
                                                        creator_id as CreatorId,
                                                        is_deleted as IsDeleted,
                                                        creation_date as CreationDate,
                                                        update_date as UpdateDate,
                                                        is_published as IsPublished,
                                                        published_on_utc as PublishedOnUtc
                                                    from videos
                                                    where
                                                        is_deleted = false and
                                                        (creator_id = @CreatorId or @CreatorId is null) and
                                                        (creation_date >= @StartDate or @StartDate is null) and
                                                        (creation_date <= @EndDate or @EndDate is null)
                                                    order by creation_date
                                                    limit @PageSize
                                                    offset @Page";
        
    }

    

}