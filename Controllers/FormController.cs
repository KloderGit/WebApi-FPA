using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApi.Models;
using WebApiBusinessLogic;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Form")]
    public class FormController : Controller
    {
        BusinessLogic logic;
        ILogger logger;

        public FormController(ILogger logger, BusinessLogic logic)
        {
            this.logger = logger;
            this.logic = logic;
        }


        // POST: api/Form/addlead
        [HttpPost]
        [Route("addlead")]
        public void Post([FromBody]IncomingLeadViewModel value)
        {
            logger.Information( "Модель формы: {@Model}", value );
        }


    }
}
