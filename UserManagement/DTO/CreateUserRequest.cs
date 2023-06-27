using System.ComponentModel.DataAnnotations;
using UserManagement.Const;

namespace UserManagement.DTO
{
    public class CreateUserRequest
    {
        public Guid Id { get; init; }
        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringErrorMessage)]
        public required string Name { get; set; }

        [StringLength(ValidationConstants.InputStringLength, ErrorMessage =ValidationConstants.InputStringErrorMessage)]
        public required string Position { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(ValidationConstants.PhoneNumberRegex, ErrorMessage = ValidationConstants.InvalidPhoneNumberErrorMessage)]
        public required string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(ValidationConstants.EmailValidationRegex, ErrorMessage = ValidationConstants.InvalidEmailErrorMessage)]
        public required string UserName { get; set; }

        [StringLength(ValidationConstants.InputStringLength, ErrorMessage = ValidationConstants.InputStringErrorMessage)]
        public required string Password { get; init; }

        public required Guid CustomerId { get; init; }
    }
}
