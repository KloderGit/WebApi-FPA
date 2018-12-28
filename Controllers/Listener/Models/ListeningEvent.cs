using Domain.Models.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.Listener.Models
{
    public class ListeningEvent
    {
        IEnumerable<Lead> Update { get; set; }
    }
}
