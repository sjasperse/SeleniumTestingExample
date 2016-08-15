using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;

namespace SeleniumExample.Web.E2ETests
{
	public static class WebDriverExtensions
	{
		public static IWebDriver WasSuccessful(this IWebDriver driver)
		{
			var currentUrl = driver.Url;

			// 404 check
			var body = driver.FindElement(By.TagName("body"));
			if (string.IsNullOrEmpty(body.Text))
			{
				throw new Exception($"'{driver.Url}' did not appear to load successfully");
			}

			return driver;
		}
	}
}