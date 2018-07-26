using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using RushOrders.Core.Models;
using Xunit;

namespace RushOrders.Tests.Integration
{
    public class OrderControllerShould : IntegrationTestsBase
    {
        [Fact]
        public async Task BeFoundWhenInserted()
        {  //Act
            IEnumerable<Customer> customers = await _customerService.GetAllAsync();
            Customer customer = customers.FirstOrDefault();
            Order sut = new Order()
            {
                Price = new decimal(456.96)
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(sut),
                Encoding.UTF8, "application/json");
            HttpResponseMessage responsePost = await _client.PostAsync($"/api/customer/{customer.Id}/orders", content);

            responsePost.EnsureSuccessStatusCode();

            var expectedOrder = await _orderService.GetOrdersByCustomerIdAsync(customer.Id);
            HttpResponseMessage response = await _client.GetAsync($"/api/customer/{customer.Id}/orders");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            var insertedOrders = JsonConvert.DeserializeObject<List<Order>>(responseString);

            // Assert - Using FluentAssertions
            expectedOrder.FirstOrDefault()
                .Should()
                .BeEquivalentTo(
                insertedOrders.FirstOrDefault(),
                i => i.Excluding(p => p.Id)
                .Excluding(p => p.Customer));
        }

        [Fact]
        public async Task HaveAValidCustomer()
        {
            //Act
            IEnumerable<Customer> customers = await _customerService.GetAllAsync();
            Customer customer = customers.FirstOrDefault();

            var orders = await _orderService.GetOrdersByCustomerIdAsync(customer.Id);

            foreach (Order item in orders)
                //Assert
                Assert.Equal(customer.Id, item.Customer.Id);
        }

        [Theory]
        [InlineData(109)]
        [InlineData(1099)]
        [InlineData(99)]
        [InlineData(77)]
        public async Task ReturnNotFoundWhenInvalidCustomer(int customerId)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync($"/api/customer/{customerId}/orders");

            //Assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhenPriceIsInvalid()
        {
            // Act
            Order sut = new Order()
            {
                Price = -1
            };

            IEnumerable<Customer> customers = await _customerService.GetAllAsync();
            Customer customer = customers.FirstOrDefault();

            StringContent content = new StringContent(JsonConvert.SerializeObject(sut),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"/api/customer/{customer.Id}/orders", content);

            //Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        } 

        [Fact]
        public async Task ReturnOkWhenValid()
        {
            //Act
            var customers = await _customerService.GetAllAsync();
            Customer customer = customers.FirstOrDefault();
            await _orderService.AddAsync(new Order() { Price = new decimal(180.66) }, customer.Id);

            HttpResponseMessage response = await _client.GetAsync($"/api/customer/{customer.Id}/orders");

            response.EnsureSuccessStatusCode();

            //Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
