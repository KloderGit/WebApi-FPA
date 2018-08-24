using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common.Models.AmoCrm.Listener;

namespace WebApi.Common.Models
{
    [ModelBinder(typeof(ListenedEventModelBinderApp))]
    public class ListenedEvent
    {
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public List<ChangedParam> Events { get; set; }
        public string ContactType { get; set; }
        public string Account { get; set; }
    }
}
