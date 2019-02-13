using Common.Mapping;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers.Listener.LocalMaps;
using WebApi.Controllers.Listener.Models;
using WebApiBusinessLogic.Logics.Listener;
using WebApiBusinessLogic.Logics.Listener.DTO;

namespace WebApi.Controllers.Listener
{
    [Produces("application/json")]
    [Route("api/Listener2")]
    public class ListenerController : Controller
    {
        TypeAdapterConfig mapper;
        ILogger logger;

        ListenerLogic logic = new ListenerLogic();

        public ListenerController(TypeAdapterConfig mapper, ILoggerFactory loggerFactory)
        {
            this.mapper = mapper;
            new RegisterCommonMaps(mapper);
            new IncomingEventToDTO(mapper);

            this.logger = loggerFactory.CreateLogger(this.ToString());
        }

        [HttpPost]
        public IActionResult Post([ModelBinder(typeof(EventsModelBinder))] IEnumerable<EventViewModel> value)
        {
            var model = value.Adapt<IEnumerable<EventDTO>>(mapper);

            logger.LogInformation("LISTENER2 - Событие AmoCRM {@value} | DTO {@model}", value.First(), model.First());

            logic.EventsHandle(model);


            return Ok(model);
        }
    }
}