using System;
using UserApplication.Models;

namespace UserApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ProductService();
            var ui = new ProductUI(service);

            while (true)
            {

                ui.PrintMenu();

                var input = int.TryParse(Console.ReadLine(), out int choiceInt) ;
                var choice = (ProductOpertation) choiceInt;

                switch (choice)
                {
                    case ProductOpertation.AddProduct: 
                        ui.AddProductUI();
                        break;
                    case ProductOpertation.UpdateProduct:
                        ui.UpdateProductUI();
                        break;
                    case ProductOpertation.GetProductDetailes:
                        ui.GetProductUI();
                        break;
                    case ProductOpertation.GetAllProducts: 
                        ui.ListAllProductUI();
                        break;
                    case ProductOpertation.DeleteProduct
                    : ui.DeleteProductUI(); 
                        break;
                    case ProductOpertation.Exit: return;

                    default:
                        break;
                }
            }
        }

      

    }
}
