using Newtonsoft.Json;
using RestSharp;

namespace StudyJunction.Core.ExternalApis
{
    public class MediaWikiActionService
    {
        public static void MakeMediaWikiSearchRequest(string searchQuery)
        {
            // Specify the API endpoint and parameters
            string apiUrl = "https://en.wikipedia.org/w/api.php";
            string action = "query";
            string list = "search";
            string format = "json";
            int srlimit = 1;

            // Create RestClient
            var client = new RestClient(apiUrl);

            // Create RestRequest
            var request = new RestRequest();
            request.AddParameter("action", action);
            request.AddParameter("list", list);
            request.AddParameter("srsearch", searchQuery);
            request.AddParameter("format", format);
            request.AddParameter("srlimit", srlimit);

            // Execute the request
            var response = client.Execute(request);

            // Check if the request was successful (status code 200)
            if (response.IsSuccessful)
            {
                // Now you can process the response (e.g., deserialize JSON)
                Console.WriteLine(response.Content);
                Console.WriteLine(response.ResponseUri);

                var result = JsonConvert.DeserializeObject<SearchResult>(response.Content);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.StatusDescription}");
            }
        }
    }

    public class SearchResult
    {
        public string BatchComplete { get; set; }
        public Continue Continue { get; set; }
        public Query Query { get; set; }
    }

    public class Continue
    {
        [JsonProperty("sroffset")]
        public int SrOffset { get; set; }
        public string ContinueValue { get; set; }
    }

    public class Query
    {
        public SearchInfo SearchInfo { get; set; }
        public List<SearchResultItem> Search { get; set; }
    }

    public class SearchInfo
    {
        public int TotalHits { get; set; }
    }

    public class SearchResultItem
    {
        public int Ns { get; set; }
        public string Title { get; set; }
        public int PageId { get; set; }
        public int Size { get; set; }
        public int WordCount { get; set; }
        public string Snippet { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
