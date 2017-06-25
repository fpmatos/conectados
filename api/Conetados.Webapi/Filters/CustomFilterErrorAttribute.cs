using Conetados.Webapi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Conetados.Webapi.Filters
{    public class CustomFilterErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var userMessage = MensagensTraducaoService.Traduzir(actionExecutedContext.Exception);

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            if (!userMessage.ErroCritico)
                httpResponseMessage.StatusCode = HttpStatusCode.NotAcceptable;
            else
            {   
                //logger
            }

            httpResponseMessage.ReasonPhrase = userMessage.Mensagem;

            actionExecutedContext.Response = httpResponseMessage;
        }
    }
}