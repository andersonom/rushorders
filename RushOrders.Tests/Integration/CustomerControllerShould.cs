using System.Net.Http;
using System.Threading.Tasks;
using RushOrders.Core.Models;
using Xunit;
using Newtonsoft.Json;
using RushOrders.Tests.Fixture;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FluentAssertions;

namespace RushOrders.Tests.Integration
{
    public class CustomerControllerShould : IntegrationTestsBase
    { 
        [Fact]
        public async Task BeFoundWhenInserted()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/api/customer");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();

            var insertedCustomer = JsonConvert.DeserializeObject<List<Customer>>(responseString)
                .FirstOrDefault(i => CustomerFixtures.GetCustomerList.FirstOrDefault()?.Name == i.Name);

            var expectedCustomer = CustomerFixtures.GetCustomerList
                .FirstOrDefault(i => CustomerFixtures.GetCustomerList.FirstOrDefault()?.Name == i.Name);

            // Assert - Using FluentAssertions
            expectedCustomer.Should().BeEquivalentTo(insertedCustomer, i => i.Excluding(p => p.Id));
        }

        [Fact]
        public async Task BeFoundByIdWhenInserted()
        {
            // Act
            var expectedCustomer = _customerService.GetAllAsync().GetAwaiter().GetResult().FirstOrDefault();

            HttpResponseMessage response = await _client.GetAsync($"/api/customer/{expectedCustomer.Id}");

            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();

            var insertedCustomer = JsonConvert.DeserializeObject<Customer>(responseString);

            // Assert - Using FluentAssertions
            expectedCustomer.Should().BeEquivalentTo(insertedCustomer, i => i.Excluding(p => p.Id));
        }

        [Fact]
        public async Task  ReturnNotFoundWhenInvalidIdProvided()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/api/customer/99");

            //Assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenEmailIsInvalid()
        {
            // Act
            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerFixtures.GetCustomerWithInvalidEmail),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/api/customer/", content);

            //Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenNameIsInvalid()
        {
            // Act
            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerFixtures.GetCustomerWithInvalidName),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/api/customer/", content);

            //Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnOkWhenValid()
        {
            // Act
            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerFixtures.GetCustomerList.FirstOrDefault()),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/api/customer/", content);

            //Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}