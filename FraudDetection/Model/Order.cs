namespace FraudModel
{
    public class Order
    {
        public long OrderId { get; set; }
        public long DealId { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }        
        public string CreditCard { get; set; }
    }
}
