using Common.Interfaces;
using LibraryAmoCRM.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApi.Controllers.SignUp.Models;
using WebApi.Infrastructure.Binders;
using WebApi.Infrastructure.LocalMaps;
using WebApiBusinessLogic.Logics.SignUp;
using WebApiBusinessLogic.Logics.SignUp.Model;

namespace WebApi.Controllers.SignUp
{
    [Produces("application/json")]
    [Route("SignUp")]
    public class SignUpController : Controller
    {
        SignUpLogic logic;
        TypeAdapterConfig mapper;
        ILoggerService logger;

        public SignUpController(ILoggerService logger, TypeAdapterConfig mapper, IDataManager crm)
        {
            this.mapper = mapper;
                new Map_FormToModel( mapper );
            this.logic = new SignUpLogic( logger, mapper, crm );
            this.logger = logger;
        }

        [HttpPost]
        [Route( "LeadFromSiteForm" )]
        public async Task<IActionResult> GivenFromSiteForm([FromBody]IEnumerable<SiteFormField> fields)
        {
            SiteFormModel model = null;

            // Convert to Model
            try
            {
                model = fields.Adapt<SiteFormModel>( mapper );

                logger.Information( GetType().Assembly.GetName().Name + " | Получена модель с форм сайта {@Model}", model );
            }
            catch (Exception ex)
            {
                logger.Error( ex, GetType().Assembly.GetName().Name + " | Ошибка маппинга модели данных формы с сайта {@Model}", model );
            }
            
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


            SignUpDTO dto = null;

            try
            {
                dto = model.Adapt<SignUpDTO>();
            }
            catch (Exception ex)
            {
                logger.Error( ex, GetType().Assembly.GetName().Name + " | Ошибка маппинга модели данных формы в модель DTO {@Model}", model );
            }

            try
            {
                var result = await logic.AddLead( dto );
                logger.Information( GetType().Assembly.GetName().Name + " | Создана сделка в AmoCRM - {@Id}", result );
                return Created( "", result );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError( "errors", ex.Message );
                return BadRequest( ModelState );
            }
            
        }
    }
}
