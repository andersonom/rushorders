using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RushOrders.Core.Models;
using RushOrders.Data.Context;
using Xunit;

namespace RushOrders.Tests
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
            _context.Customer.Add(new Customer() { Name = "Anderson Martins", Email = "andersonom@gmail.com" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task ReturnCustomers()
        {
            // Act
            var response = await _client.GetAsync("/api/customer");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(@"[{'id': 1,'name': 'Anderson Martins','email': 'andersonom@gmail.com'}]", responseString);
        }
    }
}

