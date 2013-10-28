ErikLieben.Owin.Security
========================

Microsoft [OWIN](http://owin.org/) Middleware authentication provider(s) for asp.net identity 4.5.1 

*Working on finishing the code and releasing a nuget package.*

Currently containing a provider for:

[Yammer](http://www.yammer.com)
===============================
Create a Yammer app to get a clientId and clientSecret. See [https://developer.yammer.com](http://developer.yammer.com/authentication/#a-testtoken) for more information.

Add the following to Startup.Auth.cs (VS2013)

	app.UseYammerAuthentication(
	    clientId: "",
	    clientSecret: "");