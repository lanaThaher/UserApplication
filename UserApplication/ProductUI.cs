using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Models;

namespace UserApplication
{
    public class ProductUI
    {

        private readonly ProductService _productService;

        public ProductUI(ProductService productService) {

        _productService = productService;

        }

        public void PrintMenu()
        {
            Console.WriteLine("=== Product Management ===");
            Console.WriteLine("1 - Add Product");
            Console.WriteLine("2 - Update Product");
            Console.WriteLine("3 - Get Product Details");
            Console.WriteLine("4 - List All Products");
            Console.WriteLine("5 - Delete Product ");
            Console.WriteLine("6 - Exit");
            Console.WriteLine();
        }

        public void AddProductUI()
        {
            Console.Write("Enter name: ");
            var name = Console.ReadLine();

            Console.Write("Enter price: ");
            var priceInput = Console.ReadLine();

            var result = _productService.AddProduct(name!, priceInput!);
            Console.WriteLine(result + "\n");

        }

        public void UpdateProductUI()
        {
            Console.WriteLine("\nEnter the ID of the product to update it \n");

            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Invalid ID.\n");
                return;
            }

            var product = _productService.getProductDetails(id);

            if (product == null)
            {
                Console.WriteLine("Product not found!\n");
                return;

            }

             Console.WriteLine($"The Name of the product is {product.Name}");
             Console.WriteLine($"The price of the product is {product.Price}");

            Console.Write("Enter new name (leave empty to keep current): ");
            var newName = Console.ReadLine();

            Console.Write("Enter new price (leave empty to keep current): ");
            var newPrice = Console.ReadLine();

            var result = _productService.UpdateProduct(id,newName!,newPrice!);
            Console.WriteLine(result + "\n");





        }

        public void GetProductUI()
        {
            Console.WriteLine("Enter the ID of the product to View the details ");
            var idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Invalid ID.\n");
                return;
            }

            var result  = _productService.getProductDetails(id);
            if (result == null)
            {

                Console.WriteLine("Product not found!\n");
                return;
            }

            else
            {
                Console.WriteLine($"The Name of the product is {result.Name}");
                Console.WriteLine($"The price of the product is {result.Price}");
            }
        }

        public void ListAllProductUI()
        {
            var productList = _productService.listAllProduct();

            if (!productList.Any())
            {
                Console.WriteLine("No products found");

            }

            else
            {
                Console.WriteLine("ID\tName\tPrice");
                foreach (var p in productList)
                {
                    Console.WriteLine($"{p.Id}\t{p.Name}\t{p.Price}");
                }
            }
        }

        public void DeleteProductUI()
        {
            Console.Write("Enter product ID to delete: ");
            string idInput = Console.ReadLine()!;
            if (string.IsNullOrEmpty(idInput))
            {
                Console.WriteLine("\nError: ID is required!\n");
                return;
            }

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("\nError: Invalid ID!\n");
                return;
            }

            var product = _productService.getProductDetails(id);

            var result = _productService.getProductDetails(id);
            if (result == null)
            {

                Console.WriteLine("Product not found!\n");
                return;
            }

            Console.WriteLine($"Product: {product!.Name} - ${product.Price}\n");
            Console.Write("\nAre you sure you want to delete this product? (y/n): ");
            string? confirm = Console.ReadLine()!.ToLower();

            if (confirm == "y")
            {
                _productService.DeleteProduct(id);
                Console.WriteLine("\n Product deleted successfully!\n");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.\n");
            }


        }
    }
}
