namespace UserManagement.Const
{
    public static class ValidationConstants
    {
        public const int InputStringLength = 1000;
        public const string InputStringLengthErrorMessage = $"Invalid length. Inut should be no more than 1000 characters long";

        public const string NameValidationRegex = @"^[a-zA-Z -]+$";
        public const string NameInputStringErrorMessage = @"Invalid input. Please use only Alphanumeric characters, space, or Dash (-)";

        public const string EmailValidationRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        public const string InvalidEmailErrorMessage = @"Invalid Email Address";

        public const int PhoneStringLength = 30;
        public const string PhoneNumberRegex = @"^\+?\d{1,4}?[-.\s]?\(?\d{1,3}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$";
        public const string InvalidPhoneNumberErrorMessage = @"Invalid phone numver";
    }
}
