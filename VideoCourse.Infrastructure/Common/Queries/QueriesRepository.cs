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
            where email = @email";
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
    }
    
}