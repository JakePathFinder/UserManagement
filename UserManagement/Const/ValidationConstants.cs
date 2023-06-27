namespace UserManagement.Const
{
    public static class ValidationConstants
    {
        public const int InputStringLength = 1000;
        public const string InputStringValidationRegex = @"^[a-zA-Z0-9\s _-.]+$";
        public const string InputStringErrorMessage = @"Invalid input. Please use only Alphanumeric characters, space, Underscore (_), Dash (-) or Dot(.)";

        public const string EmailValidationRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        public const string InvalidEmailErrorMessage = @"Invalid Email Address";

        public const int PhoneStringLength = 30;
        public const string PhoneNumberRegex = @"^\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$";
        public const string InvalidPhoneNumberErrorMessage = @"Invalid phone numver";
    }
}
