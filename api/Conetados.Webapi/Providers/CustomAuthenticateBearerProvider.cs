using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Conetados.Webapi.Providers
{
    public class CustomAuthenticateBearerProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            if (context.Request.Headers.Any(item => item.Key.Equals("AuthorizationConectados")))
                context.Token = context.Request.Headers["AuthorizationConectados"].Replace("Bearer ", "");

            return base.RequestToken(context);
        }

        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            return base.ValidateIdentity(context);
        }
    }
}