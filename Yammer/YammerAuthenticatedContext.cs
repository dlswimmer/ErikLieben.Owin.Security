namespace ErikLieben.Owin.Security.Yammer
{
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Provider;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Security.Claims;

    /// <summary>
    /// Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.
    /// </summary>
    public class YammerAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="YammerAuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="user">The JSON-serialized user</param>
        /// <param name="accessToken">Yammer Access token</param>
        public YammerAuthenticatedContext(IOwinContext context, JObject user, string accessToken) : base(context)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User = user;
            AccessToken = accessToken;

            JToken userId = User["user"]["id"];
            if (userId == null)
            {
                throw new ArgumentException("The user does not have an id.", "user");
            }

            this.Id = userId.Value<string>();
            this.Name = User["user"]["name"].Value<string>();
            this.FullName = User["user"]["full_name"].Value<string>();
            this.Email = User["user"]["contact"]["email_addresses"].First["address"].Value<string>();
            this.NetworkName = User["network"]["name"].Value<string>();
            this.NetworkId = User["network"]["id"].Value<int>();
            this.JobTitle = User["user"]["job_title"].Value<string>();

        }

        public JObject User { get; private set; }
        public string AccessToken { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string JobTitle { get; private set; }
        public string NetworkName { get; private set; }
        public int NetworkId { get; private set; } 
        

        public ClaimsIdentity Identity { get; set; }
        public AuthenticationProperties Properties { get; set; }

    }
}
