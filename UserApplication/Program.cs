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

                var input = int.TryParse(Console.ReadLine(), out int choiceInt) ;
                var choice = (ProductOpertation) choiceInt;

                switch (choice)
                {
                    case ProductOpertation.AddProduct: 
                        service.AddProduct();
                        break;
                    case ProductOpertation.UpdateProduct:
                        service.UpdateProduct();
                        break;
                    case ProductOpertation.GetProductDetailes:
                        service.getProductDetails();
                        break;
                    case ProductOpertation.GetAllProducts: 
                        service.listAllProduct();
                        break;
                    case ProductOpertation.DeleteProduct
                    : service.DeleteProduct(); 
                        break;
                    case ProductOpertation.Exit: return;

                    default:
                        break;
                }
            }
        }

      

    }
}
