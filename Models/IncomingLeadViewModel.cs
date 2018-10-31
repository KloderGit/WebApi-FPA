using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class IncomingLeadViewModel
    {
        public IncomingLeadViewModel(){}

        [JsonProperty (PropertyName ="NAME")]
        public string ContactName { get; set; }

        [JsonProperty(PropertyName = "PHONE")]
        public IEnumerable<string> ContactPhones { get; set; }

        [JsonProperty(PropertyName = "EMAIL")]
        public IEnumerable<string> ContactEmails { get; set; }

        [JsonProperty(PropertyName = "CITY")]
        public string ContactCity { get; set; }

        [JsonProperty(PropertyName = "EDU_NAME")]
        public string LeadName { get; set; }

        [JsonProperty(PropertyName = "DATE")]
        public DateTime? LeadDate { get; set; }

        [JsonProperty(PropertyName = "EDU_TYPE")]
        public string LeadType { get; set; }

        [JsonProperty(PropertyName = "TYPE")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "PRICE")]
        public int LeadPrice { get; set; }

        [JsonProperty(PropertyName = "GUID_EVENT")]
        public string LeadGuid { get; set; }
    }

    public class LeadFormViewModel
    {
        public IEnumerable<FormField> Fields { get; set; }
    }

    public class FormField
    {
        public FormField()
        {

        }
        public string name { get; set; }


        public string value { get; set; }
    }
}
