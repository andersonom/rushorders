using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using RushOrders.Core.Models;
using RushOrders.Data.Context;
using Xunit;
using RushOrders.Data.Repositories;
using Newtonsoft.Json;
using RushOrders.Tests.Fixture;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net;
using System.Text;
using FluentAssertions;

namespace RushOrders.Tests.Integration
{
    public class CustomerControllerShould 
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly SqlContext _context;

        public CustomerControllerShould() 
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
               .UseStartup<StartupTest>());
            _client = _server.CreateClient();

            _context = _server.Host.Services.GetService(typeof(SqlContext)) as SqlContext;
            CustomerRepository repo = new CustomerRepository(_context);

            repo.AddAsync(CustomerFixtures.GetCustomerList.FirstOrDefault());
        }

        [Fact]
        public async Task CustomerShouldBeFound()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/api/customer");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();

            var insertedCustomer = JsonConvert.DeserializeObject<List<Customer>>(responseString)
                .FirstOrDefault(i => CustomerFixtures.GetCustomerList.FirstOrDefault().Name == i.Name);

            var expectedCustomer = CustomerFixtures.GetCustomerList
                .FirstOrDefault(i => CustomerFixtures.GetCustomerList.FirstOrDefault().Name == i.Name);

            // Assert - Using FluentAssertions
            expectedCustomer.Should().BeEquivalentTo(insertedCustomer, i=> i.Excluding(p=> p.Id));
        }

        [Fact]
        public async Task CustomerShouldReturnNotFound()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/api/customer/99");

            //Assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CustomerShouldReturnBadRequestWhenEmailIsInvalid()
        {
            // Act
            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerFixtures.GetCustomerWithInvalidEmail),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/api/customer/", content);

            //Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CustomerShouldReturnBadRequestWhenNameIsInvalid()
        {
            // Act
            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerFixtures.GetCustomerWithInvalidName),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/api/customer/", content);

            //Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CustomerShouldReturnOkWhenValid()
        {
            // Act
            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerFixtures.GetCustomerList.FirstOrDefault()),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("/api/customer/", content);

            //Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CustomerShouldHasOrder()
        {
            //TODO: Fix Mongo MOCK 
        }
    }
}