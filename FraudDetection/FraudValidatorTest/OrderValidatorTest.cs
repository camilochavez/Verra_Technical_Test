using FraudModel;
using FraudValidator;
using System;
using System.Collections.Generic;
using Xunit;

namespace FraudValidatorTest
{
    public class OrderValidatorTest
    {
        private readonly IOrderValidator orderValidator;

        public OrderValidatorTest()
        {
            orderValidator = new OrderValidator();
        }
        [Fact]
        public void Validate_Fraud_Positive()
        {
            // Given
            Order order1 = new Order
            {
                DealId = 1,
                OrderId = 1,
                City = "NY",
                State = "NY",
                StreetAddress = "St. 3",
                CreditCard = "1235466",
                Zip = "123",
                Email = "example1@mail.com"
            };

            Order order2 = new Order
            {
                DealId = 1,
                OrderId = 2,
                City = "NY",
                State = "NY",
                StreetAddress = "St. 3",
                CreditCard = "12354667",
                Zip = "123",
                Email = "example.1+@mail.com"
            };

            Order order3 = new Order
            {
                DealId = 2,
                OrderId = 3,
                City = "NY",
                State = "NY",
                StreetAddress = "St. 3",
                CreditCard = "12354667",
                Zip = "123",
                Email = "example.1+@mail.com"
            };

            //When 
            var result = orderValidator.ValidateFraud(new List<Order> { order1, order2, order3 });

            //Then
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
