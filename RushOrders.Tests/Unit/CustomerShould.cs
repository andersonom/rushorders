using System;
using System.Linq;
using FluentAssertions;
using RushOrders.Core.Models;
using RushOrders.Core.Validations;
using RushOrders.Tests.Fixture;
using Xunit;
using System.Threading.Tasks;

namespace RushOrders.Tests.Unit
{
    public class CustomerShould
    {
        private readonly CustomerValidator validator;
        public CustomerShould()
        {
            //Arrange
            validator = new CustomerValidator();
        }
        [Fact]
        public void CustomerShouldBeValid()
        {
            //Act
            Customer sut = CustomerFixtures.GetCustomerList.FirstOrDefault();
            var results = validator.Validate(sut);

            //Assert
            Assert.True(results.IsValid);
        }

        [Theory]
        [InlineData("testperson@gmail.com")]
        [InlineData("TestPerson@gmail.com")]
        [InlineData("testperson+label@gmail.com")]
        [InlineData("\"Abc\\@def\"@example.com")]
        [InlineData("\"Fred Bloggs\"@example.com")]
        [InlineData("\"Joe\\Blow\"@example.com")]
        [InlineData("\"Abc@def\"@example.com")]
        [InlineData("customer/department=shipping@example.com")]
        [InlineData("$A12345@example.com")]
        [InlineData("!def!xyz%abc@example.com")]
        [InlineData("__somename@example.com")]
        [InlineData("first.last@test.co.uk")]
        public void CustomerShouldHaveAValidEmail(string email)
        {
            //Act
            Customer sut = CustomerFixtures.GetCustomerList.FirstOrDefault();
            sut.Email = email;
            var results = validator.Validate(sut);

            //Assert
            Assert.True(results.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("andersonomgmail.com")]
        [InlineData("andersonom@gmail..com")]
        public void CustomerShouldHaveInvalidEmail(string email)
        {
            //Act
            Customer sut = new Customer()
            {
                Id = 1,
                Name = "Anderson Martins",
                Email = email
            }; ;
            var results = validator.Validate(sut);

            //Assert
            Assert.False(results.IsValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void CustomerShouldHaveInvalidName(string name)
        {
            //Act 
            Customer sut = new Customer()
            {
                Id = 1,
                Name = name,
                Email = "andersonom@gmail.com"
            };

            var results = validator.Validate(sut);

            //Assert
            Assert.False(results.IsValid);
        }
        //Customer has  Name and  EmailCustomer can  have multiple  OrdersOrder can  belong to  only one  CustomerOrder has  at least  two fields:  Price and  CreatedDate.
    }
}
