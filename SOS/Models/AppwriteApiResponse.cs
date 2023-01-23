using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SOS.Models
{
    public class AppwriteResponseData
    {
        [JsonProperty("sos")]
        public bool Sos { get; set; }
    }

    public class AppwriteApiResponse
    {
        [JsonProperty("$id")]
        public string Id { get; set; }

        [JsonProperty("$createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("$updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("$permissions")]
        public List<object> Permissions { get; set; }

        [JsonProperty("functionId")]
        public string FunctionId { get; set; }

        [JsonProperty("trigger")]
        public string Trigger { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }

        [JsonProperty("stdout")]
        public string Stdout { get; set; }

        [JsonProperty("stderr")]
        public string Stderr { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }
    }
}
