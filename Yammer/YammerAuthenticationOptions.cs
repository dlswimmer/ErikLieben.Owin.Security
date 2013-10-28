namespace ErikLieben.Owin.Security.Yammer
{
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public class YammerAuthenticationOptions : AuthenticationOptions
    {
        public YammerAuthenticationOptions() : base(Resources.YammerSchemaName)
        {
            this.Caption = Resources.YammerSchemaName;
            this.CallbackPath = "/signin-yammer";
            this.AuthenticationMode = AuthenticationMode.Passive;
            this.BackchannelTimeout = TimeSpan.FromSeconds(60);
            this.Scope = new List<string>();            
        }

        public string ClientId { get; set; }
        
        public string ClientSecret { get; set; }

        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        public IYammerAuthenticationProvider Provider { get; set; }

        public TimeSpan BackchannelTimeout { get; set; }
        
        public HttpMessageHandler BackchannelHttpHandler { get; set; }
        
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        public string CallbackPath { get; set; }

        public string SignInAsAuthenticationType { get; set; }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        public IList<string> Scope { get; private set; }
    }
}
