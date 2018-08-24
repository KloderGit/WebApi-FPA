using Mapster;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IO;
using System.Net;
using System.Net.Http;
using WebApi.Common.Models;
using WebApi.Utils;
using WebApiBusinessLogic;
using WebApiBusinessLogic.Models.Crm;

namespace WebApiFPA.Controllers
{
    [Route("api/[controller]")]
    public class ListenerController : Controller
    {
        BusinessLogic logic;
        ILogger logger;

        public ListenerController(ILogger logger, BusinessLogic logic)
        {
            new RegisterMapsterConfig();

            this.logger = logger;
            this.logic = logic;
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
            //var ttt = new StreamReader(Request.Body).ReadToEndAsync().Result;

            logic.GetEvent(value.Adapt<CrmEvent>());

            logger.Information("Входящее событие, {@Element}", value);

            //var api = new MandrillApi("3GWqeBmS1HGZy9Y8FOzKJQ");
            //var message = new MandrillMessage("info@fitness-pro.ru", "kloder@fitness-pro.ru",
            //                "hello mandrill!", "...how are you?");
            //var result = await api.Messages.SendAsync(message);

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
