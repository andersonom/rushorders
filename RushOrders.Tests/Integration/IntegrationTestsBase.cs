using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RushOrders.Core.Interfaces.Repositories;
using RushOrders.Core.Interfaces.Services;
using RushOrders.Data.Context;
using RushOrders.Data.Repositories;
using RushOrders.Service;
using RushOrders.Tests.Fixture;
using RushOrders.Tests.Mock;

namespace RushOrders.Tests.Integration
{
    public abstract class IntegrationTestsBase
    {
        protected IOrderRepository _orderRepository;
        protected IOrderService _orderService;
        protected ICustomerService _customerService;
        protected TestServer _server;
        protected HttpClient _client;
        protected SqlContext _context;

        protected IntegrationTestsBase()
        {
            //Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupTest>());
            _client = _server.CreateClient();
            _context = _server.Host.Services.GetService(typeof(SqlContext)) as SqlContext;

            IOrderRepository orderMockedRepository = _server.Host.Services.GetService(typeof(IOrderRepository)) as OrderMockedRepository;
            ICustomerRepository customerRepo = new CustomerRepository(_context);

            _customerService = new CustomerService(customerRepo, orderMockedRepository);
            _orderService = new OrderService(orderMockedRepository, customerRepo);

            _customerService.AddAsync(CustomerFixtures.GetCustomerList.FirstOrDefault());
        }
    }
}
