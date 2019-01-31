using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Listener.Models;
using WebApiBusinessLogic.Logics.Listener;

namespace WebApi.Controllers.Listener
{
    [Produces("application/json")]
    [Route("api/Listener2")]
    public class ListenerController : Controller
    {
        LIstenerLogic logic = new LIstenerLogic();

        public ListenerController()
        {

        }

        [HttpPost]
        public IActionResult Post([ModelBinder(typeof(EventsModelBinder))] ListeningEvent value)
        {
            //logic.EventsHandle(null);

            return Ok();
        }
    }
}