using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common.Models;
using WebApi.Common.Models.AmoCrm.Listener;

namespace WebApi.Common
{
    public class ListenedEventModelBinderApp : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            var ResultObject = new ListenedEvent();

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var names = bindingContext.HttpContext.Request.Form.Keys.FirstOrDefault().Split("[").Select(x => x.Replace("]", "")).ToArray();
            char[] charsToTrim = { '\n', '\r', ' ' };




            //if (!bindingContext.ModelMetadata.IsComplexType)
            //{
            //    try
            //    {
            //        var propertyName = bindingContext.ModelMetadata.PropertyName;

            //        var property = bindingContext.ModelMetadata.ContainerType.GetProperty(propertyName);

            //        if (property != null)
            //        {
            //            var attribute = property.GetCustomAttributes(typeof(string), false).FirstOrDefault();
            //        }
            //    }
            //    catch (Exception exception)
            //    {
            //        var message = exception.Message;

            //        return null;
            //    }
            //}





            ResultObject.Entity = names[0].Trim(charsToTrim);

            string @event = names[1].Trim(charsToTrim);

            ResultObject.Events = GetEvents(ResultObject.Entity, @event, bindingContext);

            ResultObject.EntityId = bindingContext.ValueProvider.GetValue(ResultObject.Entity + "." + @event + "[0].id").FirstValue;

            ResultObject.ContactType = bindingContext.ValueProvider.GetValue(ResultObject.Entity + "." + @event + "[0].type").FirstValue;

            ResultObject.Account = bindingContext.ValueProvider.GetValue("account.subdomain").FirstValue;



            bindingContext.Result = ModelBindingResult.Success( ResultObject );
            
            return Task.CompletedTask;
        }

        List<ChangedParam> GetEvents(string entity, string @event, ModelBindingContext context)
        {
            var Events = new List<ChangedParam>();

            // Текущие виды параметров

            var hasOldstatus = context.ValueProvider.GetValue(entity + "." + @event + "[0]" + "." + "old_status").FirstValue;

            if (!String.IsNullOrEmpty(hasOldstatus))
            {
                var curentValue = context.ValueProvider.GetValue(entity + "." + @event + "[0]" + "." + "old_status").FirstValue;
                if (curentValue != hasOldstatus)
                {
                    Events.Add(new ChangedParam { Event = "ChangeStatus", OldValue = hasOldstatus });
                }                
            }

            var hasResponsibleUser = context.ValueProvider.GetValue(entity + "." + @event + "[0]" + "." + "old_responsible_user_id").FirstValue;

            if (!String.IsNullOrEmpty(hasResponsibleUser))
            {
                var curentValue = context.ValueProvider.GetValue(entity + "." + @event + "[0]" + "." + "old_responsible_user_id").FirstValue;
                if (curentValue != hasResponsibleUser)
                {
                    Events.Add(new ChangedParam { Event = "ChangeResponsibleUser", OldValue = hasResponsibleUser });
                }
            }

            if (Events.Count < 1)
            {
                Events.Add(new ChangedParam { Event = @event });
            }

            return Events;
        }
    }
}
