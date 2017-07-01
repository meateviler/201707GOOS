using FluentAutomation;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps
{
	[Binding]
	[Scope(Feature = "BudgetCreate")]
	public class BudgetCreateSteps : FluentTest
	{
		private readonly BudgetCreatePage _budgetCreatePage;

		public BudgetCreateSteps()
		{
			_budgetCreatePage = new BudgetCreatePage(this);
		}

		[Given(@"go to adding budget page")]
		public void GivenGoToAddingBudgetPage()
		{
			_budgetCreatePage.Go();
		}

		[When(@"I add a buget (.*) for ""(.*)""")]
		public void WhenIAddABugetFor(int amount, string yearMonth)
		{
			_budgetCreatePage
				.Amount(amount)
				.Month(yearMonth)
				.AddBudget();
		}

		[Then(@"it should display ""(.*)""")]
		public void ThenItShouldDisplay(string message)
		{
			_budgetCreatePage.ShouldDisplay(message);
		}
	}
}