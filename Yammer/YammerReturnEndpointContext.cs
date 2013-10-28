namespace ErikLieben.Owin.Security.Yammer
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Provider;

    /// <summary>
    /// Provides context information to middleware providers.
    /// </summary>
    public class YammerReturnEndpointContext : ReturnEndpointContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">OWIN environment</param>
        /// <param name="ticket">The authentication ticket</param>
        public YammerReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context, ticket) { }
    }
}
