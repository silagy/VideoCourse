using System.Security.Cryptography;
using ErrorOr;
using VideoCourse.Domain.Enums;

namespace VideoCourse.Domain.DomainErrors;

public static class CustomErrors
{
    public static class Entity
    {
        public static Error EmptyGuid => Error.Validation(
            code: "Entity.EmptyGuid",
            description: "The Id property cannot be empty");
        
        public static Error EntityNotFound => Error.NotFound(
            code: "Entity.NotFound",
            description: "Entity not found");
        
        public static Error EntityNotNull => Error.NotFound(
            code: "Entity.NotNullOrEmpty",
            description: "Entity cannot be null or empty");

        public static Error EntityCannotBeDeleted => Error.Failure(
            code: "Entity.CannotBeDeleted",
            description: "Error occured while deleting the entity");
    }

    public static class User
    {
        public static Error UserExists => Error.Failure(
            code: "User.Exists",
            description: "User already exists");

        public static Error UserNotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found");

        public static Error UserPasswordInCorrect => Error.Failure(
            code: "User.PasswordInCorrect",
            description: "User password incorrect");

        
        
    }

    public static class Role
    {
        public static Error RolesNotFound => Error.Failure(
            code: "User.RolesNotFound",
            description: "Roles not found");
    }

    public static class Video
    {
        public static Error SectionNameAlreadyExists => Error.Conflict(
            code: "Section.AlreadyExists",
            description:"Section with the same name already exists");
        
        public static Error SectionStartTimeMustBeSequential => Error.Conflict(
            code: "Section.StartTimeMustBeSequential",
            description:"Section start time must be sequential, make sure you create the sections in sequential manner");
        
        public static Error CreatorNotFound => Error.NotFound(
            code: "Video.CreatorNotFound",
            description: "Creator not found");

        public static Error ItemExistsOnThatTime => Error.Failure(
            code: "Item.ExistsOnTheSameTime",
            description: "There is another item on the same time, please select another time");

        public static Error ItemIsGreaterThanVideoDuration => Error.Failure(
            code: "Item.IsGreaterThanVideoDuration",
            description: "Item cannot be greater than the video duration");
        
        public static Error VideoIsAlreadyPublished => Error.Failure(
            code: "Video.IsAlreadyPublished",
            description:"The video is already published");
    }
    
    public static class Section
    {
        public static Error IsEqualOrGreaterThanEndTime(int endTime) => Error.Validation(
            code: "StartTime.IsEqualOrGreaterThanEndTime",
            description: $"The start time must be less than end time {endTime}");
    }

    public static class Question
    {
        public static Error MultipleAnswersSingleSelectionMustHaveOneRightAnswer => Error.Failure(
            code: "Question.MultipleAnswersSingleSelectionMustHaveOnlyOneRightAnswer",
            description: "Question of type multiple answers with single section must have only one right answer");
        
    }

    public static class Email
    {
        public static Error EmailNotValid(string value) => Error.Validation(
            code: "Email.IsNotValidEmail",
            description: $"The provided {value} is not a valid email");
    }
    
    public static class Duration
    {
        public static Error DurationIsNotPositive(int minDuration) => Error.Validation(
        code: "Duration.IsNotPositive",
        description: $"The duration max be greater than {minDuration}");
    }
    
    public static class Url
    {
        public static Error NotValidUrl => Error.Validation(
            code: "Value.IsNotUrl",
            description: "The provided value is not URL");
        
        public static Error TooShort(int minLength) => Error.Validation(
            code: "VideoUrl.IsToShort",
            description: $"The video url must be more then {minLength}");
    }

    public static class Permission
    {
        public static Error PermissionAlreadyExists(Permissions permission) => Error.Validation(
            code: "Permission.AlreadyExists",
            description: $"The permission ${permission.ToString()} is already exists");
    }
}