using System.ComponentModel.DataAnnotations;
using UserManagement.Const;

namespace UserManagement.DTO
{
    public class CreateUserRequest : IIdEntityDto
    {
        public Guid Id { get; set; }
        
        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringLengthErrorMessage)]
        [RegularExpression(ValidationConstants.NameValidationRegex, ErrorMessage = ValidationConstants.NameInputStringErrorMessage)]
        public string? FirstName { get; set; }

        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringLengthErrorMessage)]
        [RegularExpression(ValidationConstants.NameValidationRegex, ErrorMessage = ValidationConstants.NameInputStringErrorMessage)]
        public string? LastName { get; set; }

        [RegularExpression(ValidationConstants.PhoneNumberRegex, ErrorMessage = ValidationConstants.InvalidPhoneNumberErrorMessage)]
        public string? PhoneNumber { get; set; }

        [RegularExpression(ValidationConstants.EmailValidationRegex, ErrorMessage = ValidationConstants.InvalidEmailErrorMessage)]
        public string? Email { get; set; }

        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringLengthErrorMessage)]
        public string? Password { get; set; }
    }
}
