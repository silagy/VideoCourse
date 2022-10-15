using System.Security.Cryptography;
using ErrorOr;

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

    public static class Video
    {
        public static Error SectionNameAlreadyExists => Error.Conflict(
            code: "Section.AlreadyExists",
            description:"Section with the same name already exists");
        
        public static Error SectionStartTimeMustBeSequential => Error.Conflict(
            code: "Section.StartTimeMustBeSequential",
            description:"Section start time must be sequential, make sure you create the sections in sequential manner");
    }
}