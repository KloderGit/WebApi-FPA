using Common.Interfaces;
using Common.Logging;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using WebApi.Common.Models;
using WebApi.Utils;
using WebApiBusinessLogic;
using WebApiBusinessLogic.Infrastructure;
using WebApiBusinessLogic.Models.Crm;

namespace WebApiFPA.Controllers
{
    [Route("api/[controller]")]
    public class ListenerController : Controller
    {
        BusinessLogic logic;
        ILoggerService logger;
        TypeAdapterConfig mapper;

        public ListenerController(ILoggerService logger, BusinessLogic logic, TypeAdapterConfig mapper)
        {
            new RegisterMapsterConfig();

            this.logger = logger;
            this.logic = logic;
            this.mapper = mapper;
        }

        // POST api/Listener
        [HttpPost]
        //public HttpResponseMessage Post()
        public HttpResponseMessage Post(ListenedEvent value)
        //public HttpResponseMessage Post(
        //    Dictionary<string, 
        //        Dictionary<string, 
        //            Dictionary<int, 
        //                Dictionary<string,string>>>>
        //    value)
        {

            logic.GetEvent(value.Adapt<CrmEvent>());

            logger.Information("Входящее событие, {@Element}", value);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // POST api/Listener/raw
        [HttpPost]
        [Route("raw")]
        public HttpResponseMessage Post()
        {
            var ttt = new StreamReader(Request.Body).ReadToEndAsync().Result;

            logger.Information("Данные от AMO, {Data}", ttt.ToString());

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }

}
