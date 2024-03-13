using FraudModel;
using System.Collections.Generic;

namespace FraudValidator
{
    public interface IOrderValidator
    {
        List<long> ValidateFraud(IEnumerable<Order> orders);
    }
}
