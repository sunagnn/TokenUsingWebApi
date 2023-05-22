using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TokenUsingWebApi;

[assembly: OwinStartup(typeof(TokenUsingWebApi.App_Start.Startup))]

namespace TokenUsingWebApi.App_Start
{
    public class Startup
    {
        //public void Configuration(IAppBuilder app)
        //{
        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        //}
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }

        void ConfigureOAuth(IAppBuilder app)
        {
            //TimeSpan time1 = TimeSpan.FromDays(5);
            //TimeSpan time2 = TimeSpan.FromHours(4);
            //TimeSpan time3 = TimeSpan.FromMinutes(3);
            //TimeSpan time4 = TimeSpan.FromSeconds(2);
            //TimeSpan time5 = TimeSpan.FromMilliseconds(1);
            //TimeSpan time6 = TimeSpan.FromTicks(10);

            //Token üretimi için authorization ayarlarını belirliyoruz.
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"), //Token talebini yapacağımız dizin
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1), //Oluşturulacak tokenı bir gün geçerli olacak şekilde ayarlıyoruz.
                AllowInsecureHttp = true, //Güvensiz http portuna izin veriyoruz.
                Provider = new ProviderAuthorization() //Sağlayıcı sınıfını belirtiyoruz. Birazdan bu sınıfı oluşturacağız.
            };

            //Yukarıda belirlemiş olduğumuz authorization ayarlarında çalışması için server'a ilgili OAuthAuthorizationServerOptions tipindeki nesneyi gönderiyoruz.
            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);

            //Authentication Type olarak Bearer Authentication'ı kullanacağımızı belirtiyoruz.
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //Bearer Token, OAuth 2.0 ile gelen standartlaşmış bir token türüdür.
            //Herhangi bir kriptolu veriye ihtiyaç duyulmaksızın client tarafından token isteğinde bulunulur ve server belirli bir zamana sahip access_token üretir.
            //Bearer Token SSL güvenliğine dayanır.
        }
    }
}
