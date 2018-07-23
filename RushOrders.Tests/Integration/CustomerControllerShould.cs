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

            repo.AddAsync(CustomerFixtures.GetCustomerList.FirstOrDefault()).GetAwaiter().GetResult();
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
            expectedCustomer.Should().BeEquivalentTo(insertedCustomer);
        }

        [Fact]
        public async Task CustomerShouldReturnNotFound()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/api/customer/99");
             
            Assert.Equal((int)response.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CustomerShouldReturnBadRequest()
        { 

        }

        [Fact]
        public async Task CustomerShouldHasOrder()
        {

        }
    }
}