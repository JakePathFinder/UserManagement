using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using UserManagement.Const;

namespace UserManagement.DTO
{
    public class CreateUserRequest
    {
        public Guid Id { get; init; }
        
        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringLengthErrorMessage)]
        [RegularExpression(ValidationConstants.NameValidationRegex, ErrorMessage = ValidationConstants.NameInputStringErrorMessage)]
        public required string FirstName { get; set; }

        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringLengthErrorMessage)]
        [RegularExpression(ValidationConstants.NameValidationRegex, ErrorMessage = ValidationConstants.NameInputStringErrorMessage)]
        public required string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(ValidationConstants.PhoneNumberRegex, ErrorMessage = ValidationConstants.InvalidPhoneNumberErrorMessage)]
        public required string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(ValidationConstants.EmailValidationRegex, ErrorMessage = ValidationConstants.InvalidEmailErrorMessage)]
        public required string Email { get; set; }

        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringLengthErrorMessage)]
        public required string Password { get; init; }
    }
}
