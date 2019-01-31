using Domain.Models.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBusinessLogic.Logics.Listener.DTO;

namespace WebApi.Controllers.Listener.Models
{
    public class ListeningEvent
    {
        public List<CrmEventDTO> CrmEvents { get; set; } = new List<CrmEventDTO>();
    }
}
