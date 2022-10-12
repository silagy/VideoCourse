using ErrorOr;

namespace VideoCourse.Domain.DomainErrors;

public static class CustomErrors
{
    public static class Entity
    {
        public static Error EmptyGuid => Error.Validation(
            code: "Entity.EmptyGuid",
            description: "The Id property cannot be empty");
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
}