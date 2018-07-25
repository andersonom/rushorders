﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Core.Models;
using RushOrders.Data.Context;
using RushOrders.Data.Repositories;
using RushOrders.Service;
using RushOrders.Tests.Fixture;
using RushOrders.Tests.Mock;
using Xunit;

namespace RushOrders.Tests.Integration
{
    public class OrderControllerShould : IntegrationTestsBase
    {

        public OrderControllerShould()
        {   
            //Arrange
            var customer = _customerService.GetAllAsync().GetAwaiter().GetResult().FirstOrDefault();
            _orderService.AddAsync(new Order() { Price = new decimal(180.66) }, customer.Id);
        }

        [Fact]
        public async Task OrderShouldBeFound()
        {  //Act
            var customers = await _customerService.GetAllAsync();
            var customer = customers.FirstOrDefault();

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
        public async Task OrderShouldHasAValidCustomer()
        {
            //Act
            var customers = await _customerService.GetAllAsync();
            var customer = customers.FirstOrDefault();

            var orders = await _orderService.GetOrdersByCustomerIdAsync(customer.Id);

            foreach (var item in orders)
                //Assert
                Assert.Equal(customer.Id, item.Customer.Id);
        }

        [Theory]
        [InlineData(109)]
        [InlineData(1099)]
        [InlineData(99)]
        [InlineData(77)]
        public async Task OrderShouldReturnNotFoundWhenInvalidCustomer(int customerId)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync($"/api/customer/{customerId}/orders");

            //Assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task OrderShouldReturnBadRequestWhenPriceIsInvalid()
        {
            // Act
            Order sut = new Order()
            {
                Price = -1
            };

            var customers = await _customerService.GetAllAsync();
            var customer = customers.FirstOrDefault();

            StringContent content = new StringContent(JsonConvert.SerializeObject(sut),
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"/api/customer/{customer.Id}/orders", content);

            //Assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task OrderShouldReturnOkWhenValid()
        {
            //Act
            var customers = await _customerService.GetAllAsync();
            var customer = customers.FirstOrDefault();

            HttpResponseMessage response = await _client.GetAsync($"/api/customer/{customer.Id}/orders");

            //Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
