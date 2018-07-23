using System;
using RushOrders.Core.Models;
using Xunit;

namespace RushOrders.Tests.Unit
{
    public class CustomerShould
    {
        [Fact]
        public void CustomerShouldHaveValidEmail()
        {
            Customer sut = new Customer();
            
        }
        [Fact]
        public void CustomerShouldHaveValidName()
        {
            Customer sut = new Customer();

        }
        //Customer has  Name and  EmailCustomer can  have multiple  OrdersOrder can  belong to  only one  CustomerOrder has  at least  two fields:  Price and  CreatedDate.
    }
}
