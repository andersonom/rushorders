using System;
using RushOrders.Core.Models;
using RushOrders.Core.Validations;
using Xunit;

namespace RushOrders.Tests.Unit
{
    public class OrderShould
    {
        private readonly OrderValidator validator;
        public OrderShould()
        {
            //Arrange
            validator = new OrderValidator();
        }

        [Fact]
        public void OrderShouldBeValid()
        {
            //Act
            Order sut = new Order()
            {
                Price = 1
            };
            var results = validator.Validate(sut);

            //Assert
            Assert.True(results.IsValid);
        }

        [Fact]
        public void OrderShouldBeInvalidWhenIfHaveNoCreationDate()
        {
            //Act
            Order sut = new Order()
            {
                Price = 1,
                CreationDate = DateTime.MinValue
            };
            var results = validator.Validate(sut);

            //Assert
            Assert.False(results.IsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1.1)]
        [InlineData(1.1111111)]
        public void OrderShouldHavePositivePrice(decimal price)
        {
            //Act
            Order sut = new Order()
            {
                Price = price
            };
            var results = validator.Validate(sut);

            //Assert
            Assert.True(results.IsValid);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OrderShouldBeInvalidWhenHaveNegativePrice(decimal price)
        {
            //Act
            Order sut = new Order()
            {
                Price = price
            };
            var results = validator.Validate(sut);

            //Assert
            Assert.False(results.IsValid);
        }
    }
}

