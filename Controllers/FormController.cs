using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApi.Models;
using WebApi.Utils;
using WebApiBusinessLogic;
using WebApiBusinessLogic.Models.Site;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Form")]
    public class FormController : Controller
    {
        BusinessLogic logic;
        ILogger logger;
        TypeAdapterConfig mapper;

        public FormController(ILogger logger, BusinessLogic logic, TypeAdapterConfig mapper)
        {
            this.logger = logger;
            this.logic = logic;
            this.mapper = mapper;
        }


        // POST: api/Form/addlead
        [HttpPost]
        [Route("addlead")]
        public void Post([FromBody]IncomingLeadViewModel value)
        {
            logger.Information( "Модель формы: {@Model}", value );

            var vm = value.Adapt<SignUpForEvent>();

            //var result = logic.LookForAmoUser(value.ContactPhones, value.ContactEmails.FirstOrDefault());
            try
            {
                var resut = logic.CreateLeadFormSite(vm);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
