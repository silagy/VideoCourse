using ErrorOr;

namespace VideoCourse.Application.Core.ValidationErrors;

public static class ValidationErrors
{
    public static class Video
    {
        public static Error IdIsRequired => Error.Validation(
            code: "Video.IdIsRequired",
            description: " Video Id is required");
        public static Error NameIsRequired => Error.Validation(
            code: "Video.NameIsRequired",
            description: " Video name is required");
        
        public static Error NameMinLength => Error.Validation(
            code: "Video.NameMinLength",
            description: " Video name min length is 3");

        public static Error UrlIsRequired => Error.Validation(
            code: "Video.UrlIsRequired",
            "Video Url is required");

        public static Error UrlMustBeValidUrl => Error.Validation(
            code: "Video.UrlMustBeValidUrl",
            description: "Video Url must be valid url");
        
        public static Error CreatorIdIsRequired => Error.Validation(
            code: "Video.CreatorIdIsRequired",
            "Video CreatorId is required");
        
        public static Error DurationIsRequired => Error.Validation(
            code: "Video.DurationRequired",
            "Video Duration is required");
        
        public static Error DurationMinLength => Error.Validation(
            code: "Video.DurationMinLength",
            description: " Video duration min length is 0");
    }
    
    public static class Section
    {
        
        public static Error IdIsRequired => Error.Validation(
            code: "Section.IdIsRequired",
            description: " Section Id is required");
        public static Error NameIsRequired => Error.Validation(
            code: "Section.NameIsRequired",
            description: " Section name is required");
        
        public static Error StartTimeIsRequired => Error.Validation(
            code: "Section.StartTimeIsRequired",
            description: " Section StartTime is required");
        
        public static Error StartTimeMinLength => Error.Validation(
            code: "Section.StartTimeMinLength",
            description: "Section Start Time must be more than 0");
        
        public static Error EndTimeIsRequired => Error.Validation(
            code: "Section.EndTimeIsRequired",
            description: " Section EndTime is required");
        
        public static Error EndTimeMinLength => Error.Validation(
            code: "Section.EndTimeMinLength",
            description: "Section End Time must be more than 0");
    }
    
    public static class Note
    {
        public static Error NameIsRequired => Error.Validation(
            code: "Note.NameIsRequired",
            description: "Note name is required");
        
        public static Error NameMinLength => Error.Validation(
            code: "Note.NameMinLength",
            description: "Note name min length is 3");
        
        public static Error ContentIsRequired => Error.Validation(
            code: "Note.ContentIsRequired",
            description: "Note content is required");
        
        public static Error TimeMinLength => Error.Validation(
            code: "Note.TimeMinLength",
            description: "Note time mark must be greater than 0");
        
        public static Error TimeIsRequired => Error.Validation(
            code: "Note.TimeIsRequired",
            description: "Note time is required");
        
        public static Error VideoIdIsRequired => Error.Validation(
            code: "Note.VideoIdIsRequired",
            description: "Note videoId is required");
    }

    public static class Question
    {
        public static Error NameIsRequired => Error.Validation(
            code: "Question.NameIsRequired",
            description: "Question name is required");
        
        public static Error NameMinLength => Error.Validation(
            code: "Question.NameMinLength",
            description: "Question name min length is 3");
        
        public static Error ContentIsRequired => Error.Validation(
            code: "Content.NameIsRequired",
            description: "Content name is required");
        
        public static Error ContentMinLength => Error.Validation(
            code: "Content.NameMinLength",
            description: "Content name min length is 3");
        
        public static Error TimeMinLength => Error.Validation(
            code: "Question.TimeMinLength",
            description: "Question time mark must be greater than 0");
        
        public static Error TimeIsRequired => Error.Validation(
            code: "Question.TimeIsRequired",
            description: "Question time is required");
        
        public static Error VideoIdIsRequired => Error.Validation(
            code: "Question.VideoIdIsRequired",
            description: "Question videoId is required");
        
        public static Error QuestionTypeIsEnum => Error.Validation(
            code: "Question.TypeIsNotaValidEnum",
            description: "Question type is not a valid enum");
        
        // Question Options errors
        public static Error QuestionOptionIsRequired => Error.Validation(
            code: "Question.OptionsAreRequired",
            description: "Question options are required");
        
        public static Error QuestionOptionMinLength => Error.Validation(
            code: "QuestionOption.NameMinLength",
            description: "Question Option name min length is 3");
        
        public static Error QuestionOptionIsRightRequired => Error.Validation(
            code: "Question.IsRightIsMandatory",
            description: "Question Option right or fail are mandatory");
    }
    
    public static class User
    {
        public static Error FirstNameIsRequired => Error.Validation(
            code: "User.FirstNameIsRequired",
            description: " User FirstName is required");
        
        public static Error FirstNameMinLength => Error.Validation(
            code: "User.FirstNameMinLength",
            description: " User FirstName min length is 3");
        
        public static Error LastNameIsRequired => Error.Validation(
            code: "User.LastNameIsRequired",
            description: " User FirstName is required");
        
        public static Error LastNameMinLength => Error.Validation(
            code: "User.LastNameMinLength",
            description: " User LastName min length is 3");
        
        public static Error EmailIsRequired => Error.Validation(
            code: "User.EmailIsRequired",
            description: " User Email is required");
        
        public static Error EmailIsValidEmailAddress => Error.Validation(
            code: "User.EmailIsValidEmailAddress",
            description: " User Email must be a valid email address");
        
        public static Error PasswordIsRequired => Error.Validation(
            code: "User.PasswordIsRequired",
            description: " User Password is required");
        
        public static Error PasswordMinLength => Error.Validation(
            code: "User.PasswordMinLength",
            description: " User Password min length is 6");
        
        public static Error RoleIsRequired => Error.Validation(
            code: "User.RoleIsRequired",
            description: " User Role is required");
        
        public static Error RoleMustBeValidEnum => Error.Validation(
            code: "User.RoleMustBeValidEnum",
            description: " User Role must be a valid enum");
    }
}