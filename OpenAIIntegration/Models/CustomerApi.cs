using DAL.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIIntegration.Models
{
    public class CustomerApi
    {
        [JsonProperty("nome")]
        public string FirstName { get; set; }
        [JsonProperty("cognome")]
        public string LastName { get; set; }
        [JsonProperty("datanascita")]
        public DateTime BirthDate { get; set; }
        [JsonProperty("numerofigli")]
        public byte? ChildrenNumber { get; set; }
        [JsonProperty("reddito")]
        public double? Income { get; set; }
    }
}
