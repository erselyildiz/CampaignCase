using Campaign.Application.Commands.AddCampaign;
using Campaign.Application.Commands.SetCampaignInActive;
using Campaign.Domain.Common.Enums;
using Campaign.Test.Contexts;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Campaign.Test.Tests
{
    public class IntegrationTest
    {
        private const string ApiCampaign = "/api/campaign";

        [Fact]
        public async Task AddCampaign_ShouldBeOk()
        {
            using var httpClient = new TestContext().TestClient;

            AddCampaignCommand addCampaignCommand = new()
            {
                Name = "test-campaign",
                CamapignType = CamapignType.Brand,
                DiscountType = DiscountType.Rate,
                Discount = 1,
                StartDate = DateTime.UtcNow.AddDays(1).Date,
                EndDate = DateTime.UtcNow.AddDays(5).Date,
                Quantity = 1
            };

            var response = await httpClient.PostAsync($"{ApiCampaign}",
                new StringContent(JsonConvert.SerializeObject(addCampaignCommand), Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("6356dc6926900c0555643677")]
        public async Task SetCampaignInActiveAsync_ShouldBeOk(string id)
        {
            using var httpClient = new TestContext().TestClient;

            SetCampaignInActiveCommand setCampaignInActiveCommand = new() { Id = id };

            var response = await httpClient.PostAsync($"{ApiCampaign}/set-campaign-in-active",
                new StringContent(JsonConvert.SerializeObject(setCampaignInActiveCommand), Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task SetCampaignToBasketAsync_Case1_ShouldBeSame()
        {
            using var httpClient = new TestContext().TestClient;

            string data = GetData("Case1.json");
            string result = GetData("Case1Result.json");

            var response = await httpClient.PostAsync($"{ApiCampaign}/set-campaign-to-basket",
                new StringContent(data, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();

            Assert.True(string.Compare(contents, result) == 0);
        }

        [Fact]
        public async Task SetCampaignToBasketAsync_Case2_ShouldBeSame()
        {
            using var httpClient = new TestContext().TestClient;

            string data = GetData("Case2.json");
            string result = GetData("Case2Result.json");

            var response = await httpClient.PostAsync($"{ApiCampaign}/set-campaign-to-basket",
                new StringContent(data, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();

            Assert.True(string.Compare(contents, result) == 0);
        }

        [Fact]
        public async Task SetCampaignToBasketAsync_Case3_ShouldBeSame()
        {
            using var httpClient = new TestContext().TestClient;

            string data = GetData("Case3.json");
            string result = GetData("Case3Result.json");

            var response = await httpClient.PostAsync($"{ApiCampaign}/set-campaign-to-basket",
                new StringContent(data, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();

            Assert.True(string.Compare(contents, result) == 0);
        }

        [Fact]
        public async Task SetCampaignToBasketAsync_Case4_ShouldBeSame()
        {
            using var httpClient = new TestContext().TestClient;

            string data = GetData("Case4.json");
            string result = GetData("Case4Result.json");

            var response = await httpClient.PostAsync($"{ApiCampaign}/set-campaign-to-basket",
                new StringContent(data, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();

            Assert.True(string.Compare(contents, result) == 0);
        }

        private static string GetData(string fileName)
        {
            try
            {
                string text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "TestDatas\\" + fileName));

                return text;
            }
            catch { return null; }
        }
    }
}