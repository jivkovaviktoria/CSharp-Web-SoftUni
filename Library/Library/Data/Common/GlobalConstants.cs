namespace Library.Data.Common;

public static class GlobalConstants
{
    public static class Book
    {
        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 50;
        
        public const int AuthorMinLegnth = 5;
        public const int AuthorMaxLegnth = 50;
        
        public const int DescriptionMinLength = 5;
        public const int DescriptionMaxLength = 5000;
        
        public const decimal RatingMinValue = 0.00m;
        public const decimal RatingMaxValue = 10.00m;
    }

    public static class Category
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 50;
    }
}