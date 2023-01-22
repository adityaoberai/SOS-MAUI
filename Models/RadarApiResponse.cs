using Newtonsoft.Json;
using System.Collections.Generic;

namespace Function.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("addressLabel")]
        public string AddressLabel { get; set; }

        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("countryFlag")]
        public string CountryFlag { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("stateCode")]
        public string StateCode { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("borough")]
        public string Borough { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("distance")]
        public int? Distance { get; set; }

        [JsonProperty("layer")]
        public string Layer { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double?> Coordinates { get; set; }
    }

    public class Meta
    {
        [JsonProperty("code")]
        public int? Code { get; set; }
    }

    public class RadarApiResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("addresses")]
        public List<Address> Addresses { get; set; }
    }
}
