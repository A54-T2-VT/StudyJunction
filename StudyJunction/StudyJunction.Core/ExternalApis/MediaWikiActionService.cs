using Newtonsoft.Json;
using RestSharp;
using StudyJunction.Infrastructure.Exceptions;
using System.Text.RegularExpressions;

namespace StudyJunction.Core.ExternalApis
{
    public class MediaWikiActionService
    {
        public static async Task<string[]> MakeMediaWikiSearchRequest(string searchQuery)
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

                var snippet = result.Query.Search.Select(s => CleanSnippetFromHTMLTags(s.Snippet)).ToList();

                var pageUrl = await GetPageUrl(apiUrl, result.Query.Search[0].PageId);

                Console.WriteLine(snippet[0]);
                var returnData = new string[] { snippet[0], pageUrl };
                return returnData;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.StatusDescription}");

                throw new MediaWikiException(response.StatusDescription);
            }
        }

        static async Task<string> GetPageUrl(string apiUrl, int pageId)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set up parameters for the API request
                var parameters = new System.Collections.Generic.Dictionary<string, string>
            {
                { "action", "query" },
                { "format", "json" },
                { "prop", "info" },
                { "pageids", pageId.ToString() },
                { "inprop", "url" }
            };

                // Construct the API request URL
                string requestUrl = $"{apiUrl}?{string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"))}";

                // Send the GET request
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the JSON response and extract the page URL
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    // You may want to use a JSON parsing library (e.g., Newtonsoft.Json) here
                    // For simplicity, we'll use a simple substring match for demonstration
                    int urlStartIndex = jsonResponse.IndexOf("\"fullurl\":\"") + "\"fullurl\":\"".Length;
                    int urlEndIndex = jsonResponse.IndexOf("\"", urlStartIndex);
                    string pageUrl = jsonResponse.Substring(urlStartIndex, urlEndIndex - urlStartIndex);
                    return pageUrl;
                }
                else
                {
                    // Handle the case when the API request is not successful
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
        }

        private static string CleanSnippetFromHTMLTags(string snippet)
        {

            string pattern = "<.*?>";
            string result = Regex.Replace(snippet, pattern, string.Empty);

            return result;

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
