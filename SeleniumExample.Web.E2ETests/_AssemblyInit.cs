using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumExample.Web.E2ETests
{
	[TestClass]
	public class _AssemblyInit
	{
		[AssemblyInitialize]
		public static void AssemblyInit(TestContext testContext)
		{
			_Website.Startup();
		}

		[AssemblyCleanup]
		public static void AssemblyCleanup()
		{
			_Website.Shutdown();
		}
	}
}