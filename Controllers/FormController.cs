using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Common.Interfaces;
using LibraryAmoCRM.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.SignUp.Models;
using WebApi.Infrastructure.Mappings;
using WebApiBusinessLogic.Logics.SignUp;
using WebApiBusinessLogic.Logics.SignUp.Model;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Form")]
    public class FormController : Controller
    {
        SignUpLogic logic;
        TypeAdapterConfig mapper;
        ILoggerService logger;

        public FormController(ILoggerService logger, TypeAdapterConfig mapper, IDataManager crm)
        {
            this.mapper = mapper;
            new Map_FormToModel( mapper );
            this.logic = new SignUpLogic( logger, mapper, crm );
            this.logger = logger;
        }

        [HttpPost]
        [Route( "addlead" )]
        public async Task<IActionResult> GivenFromSiteForm([FromBody]IEnumerable<SiteFormField> fields)
        {
            // Convert to Model
            var model = fields.Adapt<SiteFormModel>( mapper );
            logger.Information( GetType().Assembly.GetName().Name + " | Получена модель с форм сайта {@Model}", model );

            // Check Model
            var validateResults = new List<ValidationResult>();
            var context = new ValidationContext( model );
            if (!Validator.TryValidateObject( model, context, validateResults, true ))
            {
                foreach (var error in validateResults)
                {
                    ModelState.AddModelError( "errors", error.ErrorMessage );
                }

                return BadRequest( ModelState );
            }

            var dto = model.Adapt<SignUpDTO>();

            try
            {
                var result = await logic.AddLead( dto );
                return Created( "", result );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError( "errors", "Internal error" );
                return BadRequest( ModelState );
            }


        }
    }
}