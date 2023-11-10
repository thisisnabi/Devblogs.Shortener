namespace Devblogs.Shortener;

public static class Constants
{
    public static class Data
    {
        public static class ExceptionMessage
        {
            public const string SearchLongUrl = "Long url";
            public const string SearchShortUrl = "Short url";
            public const string FailedGenerateUniqueCode = "Failed to generate a unique short code.";
        }
        
        public static class EndPointFilterMessages
        {
            public const string InvalidUrl = "Url is not valid.";
        }
    }
}
