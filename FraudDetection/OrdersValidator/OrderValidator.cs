using FraudModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FraudValidator
{
    public class OrderValidator : IOrderValidator
    {
        public List<long> ValidateFraud(IEnumerable<Order> orders)
        {
            List<long> fraudulentOrders = new List<long>();
            var dealIds = orders.Select(order => order.DealId).Distinct().ToList();
            foreach (var dealId in dealIds)
            {
                var ordersWithSameDealId = orders.Where(order => order.DealId == dealId);
                foreach (var order in ordersWithSameDealId)
                {
                    var fraudOrders = ordersWithSameDealId.Where(o => ValidateEmail(o.Email, order.Email) && o.CreditCard != order.CreditCard).Select(o => o.OrderId);
                    if (fraudOrders.Any())
                        fraudulentOrders.AddRange(fraudOrders);
                    else
                        fraudulentOrders.AddRange(ordersWithSameDealId.Where(o => ValidateStreetAddress(o.StreetAddress, order.StreetAddress) &&
                                                                                 ValidateState(o.State, order.State) &&
                                                                                 o.City.Equals(order.City, StringComparison.InvariantCultureIgnoreCase) &&
                                                                                 o.Zip.Equals(order.Zip, StringComparison.InvariantCultureIgnoreCase) &&
                                                                                 o.CreditCard != order.CreditCard).Select(o => o.OrderId));
                }
            }
            return fraudulentOrders;
        }

        private bool ValidateEmail(string email, string emailToCompare) =>
                            NormalizeEmail(email).Equals(NormalizeEmail(emailToCompare), StringComparison.InvariantCultureIgnoreCase);


        private bool ValidateStreetAddress(string streetAddress, string streetAddressToCompare) =>
                            NormalizeAddress(streetAddress).Equals(NormalizeAddress(streetAddressToCompare), StringComparison.InvariantCultureIgnoreCase);


        private bool ValidateState(string state, string stateToCompare) =>
                            NormalizedState(state).Equals(NormalizedState(stateToCompare), StringComparison.InvariantCultureIgnoreCase);



        private string NormalizeEmail(string email)
        {
            string[] parts = email.Split('@');
            return (parts[0].Replace(".", string.Empty) + "@" + parts[1]).Replace("+", string.Empty);
        }

        private string NormalizeAddress(string address)
        {
            return address.Replace("Street", "St.").Replace("Road", "Rd.");
        }

        private string NormalizedState(string state)
        {
            return state.Replace("Illinois", "IL").Replace("California", "CA").Replace("New York", "NY");
        }
    }
}
