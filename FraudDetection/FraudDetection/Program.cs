using FileProcessor;
using FraudValidator;
using Microsoft.Extensions.DependencyInjection;

namespace FraudDetection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<IOrderValidator, OrderValidator>()            
            .BuildServiceProvider();

            var validationService = serviceProvider.GetService<IOrderValidator>();            

            var ordersToValidate = FileMapper.FileToOrder(@"C:\Users\darkc\source\repos\Verra_Technical_Test\FraudDetection\FraudDetection\OrderList.txt");
            
            var fraudulentOrders = validationService.ValidateFraud(ordersToValidate);
            System.Console.WriteLine(string.Join(',', fraudulentOrders));
        }

    }
}
