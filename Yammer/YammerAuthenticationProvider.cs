namespace ErikLieben.Owin.Security.Yammer
{
    using System;
    using System.Threading.Tasks;

    public class YammerAuthenticationProvider : IYammerAuthenticationProvider
    {
        public YammerAuthenticationProvider()
        {
            this.OnAuthenticated = c => Task.FromResult<object>(null);
            this.OnReturnEndpoint = c => Task.FromResult<object>(null);
        }

        public Func<YammerAuthenticatedContext, Task> OnAuthenticated { get; set; }
        public Func<YammerReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

        public virtual Task Authenticated(YammerAuthenticatedContext context)
        {
            return this.OnAuthenticated(context);
        }

        public virtual Task ReturnEndpoint(YammerReturnEndpointContext context)
        {
            return this.OnReturnEndpoint(context);
        }
    }
}
