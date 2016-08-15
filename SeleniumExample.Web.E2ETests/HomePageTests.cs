using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumExample.Web.E2ETests
{
	[TestClass]
	public class HomePageTests
	{
		[TestMethod]
		public void E2E_HomePage_DefaultPageLoads()
		{
			var driver = _Website
				.StartOnHomePage()
				.WasSuccessful();
		}

		[TestMethod]
		public void E2E_HomePage_IndexPageRedirectsToDefault()
		{
			var driver = _Website
				.StartOn("index.html")
				.Url
					.Should().Be(_Website.BasePath);
		}
	}
}