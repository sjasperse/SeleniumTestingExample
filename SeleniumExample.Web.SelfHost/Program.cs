using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SeleniumExample.Web.SelfHost
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var defaultPort = 9000;
			int port = int.Parse(args.GetArgument("port", defaultPort.ToString()));
			string baseAddress = $"http://localhost:{port}/";

			// Start OWIN host
			using (WebApp.Start<Startup>(url: baseAddress))
			{
				var timeout = new Timer(s =>
				{
					Console.WriteLine("Listening on {0}. Press [Enter] to exit...", baseAddress);
				});
				timeout.Change(0, 60000); // every minute.
				using (timeout)
				{
					Console.ReadLine();
				}
			}
		}
	}

	public static class Extensions
	{
		public static string GetArgument(this string[] args, string argName, string defaultValue = null)
		{
			var arg = args.FirstOrDefault(x => x.StartsWith("-Port:", StringComparison.InvariantCultureIgnoreCase));
			var value = defaultValue;

			if (arg != null)
			{
				value = arg.Substring(arg.IndexOf(':') + 1);
			}

			return value;
		}
	}
}