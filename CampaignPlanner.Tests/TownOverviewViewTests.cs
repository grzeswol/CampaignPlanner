using CampaignPlanner.Tests.Mocks;
using CampaignPlanner.ViewModels;
using System;
using Xunit;

namespace CampaignPlanner.Tests
{
    public class TownOverviewViewTests
    {
        [Fact]
        public void Towns_Not_Null_After_Construction()
        {
            var mockTownDataService = new MockTownDataService();

            var townOverviewViewModel = new TownOverviewViewModel(mockTownDataService);
            Assert.NotNull(townOverviewViewModel.Towns);
        }
    }
}
