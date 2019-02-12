using Common.Mapping;
using Mapster;
using Microsoft.AspNetCore.Mvc;
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

        LIstenerLogic logic = new LIstenerLogic();

        public ListenerController(TypeAdapterConfig mapper)
        {
            this.mapper = mapper;
            new RegisterCommonMaps(mapper);
            new IncomingEventToDTO(mapper);
        }

        [HttpPost]
        public IActionResult Post([ModelBinder(typeof(EventsModelBinder))] IEnumerable<EventViewModel> value)
        {
            var model = value.Adapt<IEnumerable<EventDTO>>(mapper);




            return Ok(model);
        }
    }
}