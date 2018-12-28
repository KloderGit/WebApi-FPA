using Domain.Models.Crm;
using LibraryAmoCRM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.Listener.Models
{
    public class ListeningEntity
    {
        [JsonProperty(PropertyName = "update")]
        IEnumerable<ContactDTO> Update { get; set; }
    }
}
