﻿using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers.SignUp.Models;

namespace WebApi.Infrastructure.Mappings
{
    public class Map_FormToModel
    {
        public Map_FormToModel(TypeAdapterConfig config)
        {
            config.NewConfig<IEnumerable<SiteFormField>, SiteFormModel>()
                .Map(
                    dest => dest.Contact,
                    src => src
                )
                .Map(
                    dest => dest.Lead,
                    src => src
                );

            config.NewConfig<IEnumerable<SiteFormField>, ContactModel>()
                    .Map( 
                        dest => dest.Name, 
                        src => src.FirstOrDefault(x=>x.Name == "DATA[NAME]") != null 
                            ? src.FirstOrDefault( x => x.Name == "DATA[NAME]" ).Value 
                            : null
                    )
                    .Map(
                        dest => dest.Phone,
                        src => src.FirstOrDefault( x => x.Name == "DATA[PHONE][] " ) != null 
                            ? src.FirstOrDefault( x => x.Name == "DATA[PHONE][] " ).Value 
                            : null
                    )
                    .Map(
                        dest => dest.Email,
                        src => src.FirstOrDefault( x => x.Name == "DATA[EMAIL][]" ) != null 
                            ? src.FirstOrDefault( x => x.Name == "DATA[EMAIL][]" ).Value 
                            : null
                    )
                    .Map(
                        dest => dest.City,
                        src => src.FirstOrDefault( x => x.Name == "DATA[CITY]" ) != null 
                            ? src.FirstOrDefault( x => x.Name == "DATA[CITY]" ).Value
                            : null
                    );

            config.NewConfig<IEnumerable<SiteFormField>, LeadModel>()
                .Map(
                    dest => dest.Name,
                    src => src.FirstOrDefault( x => x.Name == "DATA[EDU_NAME]" ) != null
                        ? src.FirstOrDefault( x => x.Name == "DATA[EDU_NAME]" ).Value
                        : null
                )
                .Map(
                    dest => dest.Date,
                    src => src.FirstOrDefault( x => x.Name == "DATA[DATE]" ) != null
                        ? DateTime.Parse( src.FirstOrDefault( x => x.Name == "DATA[DATE]" ).Value)
                        : default(DateTime)
                )
                .Map(
                    dest => dest.EducationForm,
                    src => src.FirstOrDefault( x => x.Name == "DATA[EDU_TYPE]" ) != null
                        ? src.FirstOrDefault( x => x.Name == "DATA[EDU_TYPE]" ).Value
                        : null
                )
                .Map(
                    dest => dest.EducationType,
                    src => src.FirstOrDefault( x => x.Name == "TYPE" ) != null
                        ? src.FirstOrDefault( x => x.Name == "TYPE" ).Value
                        : null
                )
                .Map(
                    dest => dest.Agree,
                    src => src.FirstOrDefault( x => x.Name == "DATA[AGREE]" ) != null
                        ? src.FirstOrDefault( x => x.Name == "DATA[AGREE]" ).Value == "1" ? true : false
                        : default(bool)
                )
                .Map(
                    dest => dest.Guid,
                    src => src.FirstOrDefault( x => x.Name == "GUID_EVENT" ) != null
                        ? src.FirstOrDefault( x => x.Name == "GUID_EVENT" ).Value
                        : null
                )
                .Map(
                    dest => dest.isPerson,
                    src => src.FirstOrDefault( x => x.Name == "DATA[SUBJECT]" ) != null
                        ? src.FirstOrDefault( x => x.Name == "DATA[SUBJECT]" ).Value == "F"
                        : default(bool?)
                )
                .Map(
                    dest => dest.Pay,
                    src => src.FirstOrDefault( x => x.Name == "pay" ) != null
                        ? src.FirstOrDefault( x => x.Name == "pay" ).Value
                        : null
                )

                .Map(
                    dest => dest.Price,
                    src => src.FirstOrDefault( x => x.Name == "DATA[PRICE]" ) != null
                        ? int.Parse( src.FirstOrDefault( x => x.Name == "DATA[PRICE]" ).Value )
                        : default( int )
                );
        }
    }
}
