using Owin;

namespace ErikLieben.Owin.Security.Yammer
{
    using Microsoft.Owin;
    using Microsoft.Owin.Logging;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.Owin.Security.Infrastructure;
    using System;
    using System.Net.Http;

    public class YammerAuthenticationMiddleware : AuthenticationMiddleware<YammerAuthenticationOptions>, IDisposable
    {
        private readonly ILogger logger;
        private readonly HttpClient httpClient;

        public YammerAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, YammerAuthenticationOptions options) : base(next, options)
        {
            if (string.IsNullOrWhiteSpace(this.Options.ClientId))
            {
                throw new ArgumentException("The 'ClientId' must be provided.", this.Options.ClientId);
            }

            if (string.IsNullOrWhiteSpace(this.Options.ClientSecret))
            {
                throw new ArgumentException("The 'ClientSecret' option must be provided.", this.Options.ClientSecret);
            }

            this.logger = app.CreateLogger<YammerAuthenticationMiddleware>();

            if (this.Options.Provider == null)
            {
                this.Options.Provider = new YammerAuthenticationProvider();
            }

            if (this.Options.StateDataFormat == null)
            {
                IDataProtector dataProtector = app.CreateDataProtector(typeof(YammerAuthenticationMiddleware).FullName, this.Options.AuthenticationType, "v1");
                this.Options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }

            if (String.IsNullOrEmpty(this.Options.SignInAsAuthenticationType))
            {
                this.Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
            }

            this.httpClient = new HttpClient(ResolveHttpMessageHandler(Options));
            this.httpClient.Timeout = this.Options.BackchannelTimeout;
            this.httpClient.MaxResponseContentBufferSize = 15000000;
        }

        protected override AuthenticationHandler<YammerAuthenticationOptions> CreateHandler()
        {
            return new YammerAuthenticationHandler(this.httpClient, this.logger);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(YammerAuthenticationOptions options)
        {
            using (HttpMessageHandler handler = options.BackchannelHttpHandler ?? new WebRequestHandler())
            {
                // If they provided a validator, apply it or fail.
                if (options.BackchannelCertificateValidator != null)
                {
                    // Set the cert validate callback
                    var webRequestHandler = handler as WebRequestHandler;
                    if (webRequestHandler == null)
                    {
                        throw new InvalidOperationException("Validator Handler Mismatch");
                    }
                    webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;
                }

                return handler;
            }
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.httpClient.Dispose();
            }
        }

    }
}
