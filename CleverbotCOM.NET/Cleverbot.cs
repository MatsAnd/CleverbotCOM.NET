using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CleverbotCOM.NET
{
    public class Cleverbot
    {
        /// <summary>
        ///     URL used for the Cleverbot connection to the API.
        /// </summary>
        public const string ApiUrl = "http://www.Cleverbot.com/getreply";

        /// <summary>
        ///     Our API-key.
        /// </summary>
        private string _key { get; set; }

        /// <summary>
        ///     Sets the unique conversation identifier.
        /// 
        ///     Set empty to start a new converation with Cleverbot.
        /// </summary>
        private string _conversationId { get; set; }


        /// <summary>
        ///     Creates a connection to the api with the specified API-key.
        /// </summary>
        /// <param name="apiKey">The API-key to Cleverbot</param>
        public Cleverbot(string apiKey)
        {
            _key = apiKey;
        }

        /// <summary>
        ///     Sends a request to Cleverbot and returns the output as a string
        /// </summary>
        /// <param name="phrase">The text to ask Cleverbot</param>
        /// <returns>The returned message from Cleverbot</returns>
        public string Ask(string phrase)
        {
            // Create the URL to the API.
            string requestUrl = RequestBuilder(phrase);

            // Send the request and get the response
            Dictionary<string, string> response = SendWebRequest(requestUrl).GetAwaiter().GetResult();

            // Check if we got an error and return this.
            if (response.ContainsKey("error"))
                return "Error " + response["status"] + ": " + response["error"];

            // Set local conversation variable.
            _conversationId = response["cs"];

            return response["output"];
        }

        /// <summary>
        ///     Internal method to build the request URL.
        /// </summary>
        /// <param name="phrase">The message to ask Cleverbot</param>
        /// <returns>The URL-call that should be made</returns>
        private string RequestBuilder(string phrase)
        {
            // Cleverbot needs the phrase to be separated by plus-signs instead of spaces... I know, it's weird...
            phrase = phrase.Replace(" ", "+");

            var request = ApiUrl + "?key=" + _key + "&input=" + phrase + "&wrapper=" + "MatsACleverbotComNET";

            // If we have an ongoing conversation add the conversation ID.
            if (!string.IsNullOrEmpty(_conversationId))
                request += "&cs=" + _conversationId;

            return request;
        }

        /// <summary>
        ///     Internal method to send request to the Cleverbot web api.
        /// </summary>
        /// <param name="url">The URL to request. Use the RequestBuider to generate.</param>
        private async Task<Dictionary<string, string>> SendWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContinueTimeout = 20000;

            // Get the response
            WebResponse webResponse = await request.GetResponseAsync();

            var reader = new StreamReader(webResponse.GetResponseStream());
            var json = await reader.ReadToEndAsync();

            // Destroy the response and the reader.
            webResponse.Dispose();
            reader.Dispose();

            // Create JSON dictionary and return.
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
