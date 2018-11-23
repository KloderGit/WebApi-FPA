using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Interfaces;
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
        ILoggerService logger;
        TypeAdapterConfig mapper;

        public FormController(ILoggerService logger, BusinessLogic logic, TypeAdapterConfig mapper)
        {
            this.logger = logger;
            this.logic = logic;
            this.mapper = mapper;
        }

        
        // POST: api/Form/addlead
        [ HttpPost]
        [Route("addlead")]
        public async Task<IActionResult> Post([FromBody]IEnumerable<FormField> value)
        {
            logger.Information( "Модель формы: {@Model}", value );

            var vm = value.Adapt<SignUpForEvent>();

            var model = new SignUpForEvent();

            model.ContactCity = value.FirstOrDefault( x => x.name == "DATA[CITY]" )?.value;
            model.ContactEmails = value.FirstOrDefault( x => x.name == "DATA[EMAIL][]" )?.value;
            model.ContactName = value.FirstOrDefault( x => x.name == "DATA[NAME]" )?.value;
            model.ContactPhones = value.FirstOrDefault( x => x.name == "DATA[PHONE][]" )?.value;
            model.EventType = value.FirstOrDefault( x => x.name == "TYPE" )?.value;
            //model.LeadDate = value.FirstOrDefault( x => x.name == "DATA[DATE]" )?.value.toDatetime();

            vm.RequestUrl = Request.Path.Value;

            int result = 0;

            try
            {
                result = await logic.CreateLeadFormSite(vm);
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Ошибка в предоставленных данных");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }
    }
}
