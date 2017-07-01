using System.Linq;
using FluentAutomation;
using GOOS_Sample.Models.DatModels;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps.Common
{
	[Binding]
	public sealed class Hooks
	{
		// For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

		[BeforeFeature]
		[Scope(Tag = "web")]
		public static void SetBrowser()
		{
			SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
		}

		[BeforeScenario]
		public void BeforeScenarioCleanTable()
		{
			CleanTableByTags();
		}

		[AfterFeature]
		public static void AfterFeatureCleanTable()
		{
			CleanTableByTags();
		}

		private static void CleanTableByTags()
		{
			var tags = ScenarioContext.Current.ScenarioInfo.Tags
				.Where(x => x.StartsWith("Clean"))
				.Select(x => x.Replace("Clean", ""));

			if (!tags.Any())
				return;

			using (var dbcontext = new NorthwindEntities())
			{
				foreach (var tag in tags)
					dbcontext.Database.ExecuteSqlCommand(string.Format("TRUNCATE TABLE [{0}]", tag));

				dbcontext.SaveChangesAsync();
			}
		}
	}
}