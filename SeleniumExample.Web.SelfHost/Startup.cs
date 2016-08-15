using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(SeleniumExample.Web.SelfHost.Startup))]

namespace SeleniumExample.Web.SelfHost
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			#region Request/Response logging

			app.Use(async (context, next) =>
			{
				Console.WriteLine("Request: " + context.Request.Uri.ToString());

				await next();

				Console.WriteLine("Response: {0} {1} {2}", context.Response.StatusCode, context.Response.ContentType, context.Response.ContentLength);
			});

			#endregion Request/Response logging

			#region Redirections

			app.Use((context, next) =>
			{
				if (context.Request.Uri.AbsolutePath.ToLower()
					== "/index.html")
				{
					context.Response.StatusCode = 301;
					context.Response.Headers.Set("Location", "/");

					return Task.FromResult<object>(null);
				}

				return next();
			});

			#endregion Redirections

			#region Static files

			{
				var options = new FileServerOptions()
				{
					EnableDefaultFiles = true,
					EnableDirectoryBrowsing = false
				};
				options.StaticFileOptions.FileSystem = new PhysicalFileSystem(@".\wwwroot");
				options.StaticFileOptions.ServeUnknownFileTypes = false;

				app.UseDefaultFiles();
				app.UseFileServer(options);
			}

			#endregion Static files

			#region WebApi

			{
				// Configure Web API for self-host.
				HttpConfiguration config = new HttpConfiguration();
				config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
				);

				app.UseWebApi(config);
			}

			#endregion WebApi
		}
	}
}