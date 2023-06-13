namespace Contacts.Data.Common;

public static class GlobalConstants
{
    public static class UserConstants
    {
        public const int UserNameMinLength = 5;
        public const int UserNameMaxLength = 20;

        public const int EmailMinLength = 10;
        public const int EmailMaxLength = 60;

        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;
    }

    public static class ContactConstants
    {
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 50;

        public const int LastNameMinLength = 5;
        public const int LastNameMaxLength = 50;
        
        public const int EmailMinLength = 10;
        public const int EmailMaxLength = 60;

        public const int PhoneNumberMinLength = 10;
        public const int PhoneNumberMaxLength = 13;

        public const string PhoneNumberFormat = @"^(?:\+359|0)\s?\d{3}(?:[-\s]?\d{2}){3}$";
        public const string WebsiteFormat = @"^www\.[a-zA-Z0-9\-]+\.[bB][gG]$";
    }
}
