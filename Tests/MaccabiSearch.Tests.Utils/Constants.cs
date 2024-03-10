namespace MaccabiSearc.Tests.Utils
{
    public class Constants
    {
        public const int DefaultPageSize = 5;
        public const int DefaultPageNumber = 1;

        public const string ItsWorking = "It's working";
        public const string MockServiceKey = "MockServiceKey";

        public const string GoogleSearchEngineBaseUrl = "https://www.googleapis.com";
        public const string GoogleSearchEngineId = "PLEASE_INSERT_HERE_THE_GOOGLE_SEARCH_ENGINE_ID";
        public const string GoogleSearchEngineApiKey = "PLEASE_INSERT_HERE_THE_GOOGLE_SEARCH_ENGINE_API_KEY";
        public const string GoogleSearchEngineEndpointFormat = "customsearch/v1/?key={0}&cx={1}&q={{0}}&num={{1}}&start={{2}}";
    }
}
