﻿using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TokenUsingWebApi.App_Start
{
    public class ProviderAuthorization : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Domainler arası etkileşim ve kaynak paylaşımını sağlayan ve bir domainin bir başka domainin 
            //kaynağını kullanmasını sağlayan CORS ayarlarını set ediyoruz.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Kullanıcının access_token alabilmesi için gerekli validation işlemlerini yapıyoruz.
            if (context.UserName == "Suna" && context.Password == "denemePass")
            {
                ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("hata", "Kullanıcı adı veya şifre hatalı.");
            }
        }
    }
}