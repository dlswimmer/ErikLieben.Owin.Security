using Owin;

namespace ErikLieben.Owin.Security.Yammer
{
    using System;

    public static class YammerAuthenticationExtensions
    {
        public static IAppBuilder UseYammerAuthentication(this IAppBuilder app, YammerAuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            app.Use(typeof(YammerAuthenticationMiddleware), app, options);
            return app;
        }

        public static IAppBuilder UseYammerAuthentication(
            this IAppBuilder app,
            string clientId,
            string clientSecret)
        {
            return UseYammerAuthentication(
                app,
                new YammerAuthenticationOptions
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                });
        }
    }
}
