using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumExample.Web.E2ETests
{
	public static class _Website
	{
		private static string baseAddress;
		private static Process webProcess;
		private static IWebDriver webDriver;

		public static void Startup()
		{
			var port = (new Random()).Next(1000) + 9000;
			baseAddress = $"http://localhost:{port}/";
			webProcess = Process.Start(
				@"..\..\..\SeleniumExample.Web.SelfHost\bin\debug\SeleniumExample.Web.SelfHost.exe",
				$"-port:{port}"
			);

			webDriver = new ChromeDriver();
		}

		public static IWebDriver StartOn(string relativeUrl)
		{
			webDriver.Navigate().GoToUrl(baseAddress + relativeUrl);
			return webDriver;
		}

		public static IWebDriver StartOnHomePage()
		{
			return StartOn("/");
		}

		public static string BasePath { get { return baseAddress; } }

		public static void Shutdown()
		{
			webDriver.Quit();
			webDriver.Dispose();

			webProcess.Kill();
			webProcess.Dispose();
		}
	}
}