﻿using Common.Interfaces;
using LibraryAmoCRM.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApi.Controllers.SignUp.Models;
using WebApi.Infrastructure.Binders;
using WebApi.Infrastructure.Mappings;
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
            // Convert to Model
            var model = fields.Adapt<SiteFormModel>(mapper);
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