namespace MovieLibrary.Data
{
    public class DataConstants
    {
        public class Movie
        {
            public const int TitleMaxLength = 100;
            public const int TitleMinLength = 2;           
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;
            public const int YearMinValue = 1875;
            public const int YearMaxValue = 2050;
            public const int RuntimeMinValue = 1;
            public const int RuntimeMaxValue = 999;
        }
        public class Genre
        {
            public const int NameMaxLength = 25;
        }
        
        public class TicketSeller
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }
        
        public class ImageUrl
        {
            public const int UrlMaxLength = 2048;
        }

        

        
    }
}
