using System;
using UserApplication.Models;

namespace UserApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ProductService();

            while (true)
            {

                Console.WriteLine("\nProduct Menu : \n");
                Console.WriteLine("1 Add Product");
                Console.WriteLine("2 Update Product");
                Console.WriteLine("3 Get Product Details");
                Console.WriteLine("4 List All  Products");
                Console.WriteLine("5 Delete Product");
                Console.WriteLine("6 Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": service.AddProduct(); break;
                    case "2": service.UpdateProduct(); break;
                    case "3": service.getProductDetails(); break;
                    case "4": service.listAllProduct(); break;
                    case "5": service.deleteProduct(); break;
                    case "6": return;

                    default:
                        break;
                }
            }
        }

      

    }
}
