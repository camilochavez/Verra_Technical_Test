using FraudModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileProcessor
{
    public static class FileMapper
    {
        public static IEnumerable<Order> FileToOrder(string filePath)
        {
            try
            {
                return from line in File.ReadAllLines(filePath).Skip(1)
                       let columns = line.Split(',')
                       select new Order
                       {
                           OrderId = long.Parse(columns[0]),
                           DealId = long.Parse(columns[1]),
                           Email = columns[2],
                           StreetAddress = columns[3],
                           City = columns[4],
                           State = columns[5],
                           Zip = columns[6],
                           CreditCard = columns[7]
                       };
            }
            catch (IOException)
            {
                System.Console.WriteLine("File cannot be read");
                return Enumerable.Empty<Order>();
            }
        }
    }
}
