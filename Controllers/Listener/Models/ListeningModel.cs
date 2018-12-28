using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.Listener.Models
{
    public class ListeningModel
    {
        [JsonProperty(PropertyName = "contacts")]
        ListeningEntity Contacts { get; set; }
    }
}
