using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Models;
using UserApplication.Repository;


namespace UserApplication
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            var context = new ApplicationDbContext();
            _productRepository = new ProductRepository(context);
        }

        public void AddProduct()
        {

            Console.Write("Enter name: ");
            var name = Console.ReadLine();

            Console.Write("Enter price: ");
            var priceInput = Console.ReadLine();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(priceInput))
            {
                Console.WriteLine("\nError: Name and price are required!\n");
                return;
            }

            if (!double.TryParse(priceInput, out double price))
            {
                Console.WriteLine("\nError: Price must be a valid number!\n");
                return;
            }

            if (_productRepository.IsNameExist(name))
            {
                Console.WriteLine("\nError: a product with this name already exists\n");
            }
            else
            {
                var product = new Product { Name = name, Price = price };
                _productRepository.Add(product);

                Console.WriteLine("\nProduct added successfully\n");
            }
        }

        public void UpdateProduct()
        {

            Console.WriteLine("\nEnter the ID of the product to update it \n");
            string? idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Error: Invalid ID!\n");
                return;
            }

            var product = _productRepository.GetById(id);

            if (product == null)
            {
                Console.WriteLine("Product not found!\n");
                return;
            }

            Console.WriteLine($"The Name of the product is {product.Name}");
            Console.WriteLine($"The price of the product is {product.Price}");

            Console.Write("Enter new name (leave empty to keep current): ");
            string? newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName) )
            {
                bool nameExists = _productRepository.IsNameAndIdExist(newName, id);
                if (nameExists)
                {
                    Console.WriteLine("Error: a product with this name already exists!\n");
                    return;
                }
                product.Name = newName;
            }

            Console.Write("Enter new price (leave empty to keep current): ");
            string? newPriceInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPriceInput))
            {
                if (!double.TryParse(newPriceInput, out double newPrice))
                {
                    Console.WriteLine("Error: Price must be a valid number!\n");
                    return;
                }
                product.Price = newPrice;
            }


            _productRepository.Update(product);
            Console.WriteLine("Product updated successfully\n");
        }

        public void getProductDetails()
        {

            Console.WriteLine("Enter the ID of the product to View the details ");
            int id = Convert.ToInt32(Console.ReadLine());

            var product = _productRepository.GetById(id);

            if (product == null)
            {

                Console.WriteLine("Product not found!\n");
                return;
            }

            else
            {
                Console.WriteLine($"The Name of the product is {product.Name}");
                Console.WriteLine($"The price of the product is {product.Price}");
            }
        }

        public void listAllProduct()
        {

            var productList = _productRepository.GetAll();

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


        public void DeleteProduct()
        {

            Console.Write("Enter product ID to delete: ");
            string? idInput = Console.ReadLine();

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

            var product = _productRepository.GetById(id);
            if (product == null)
            {
                Console.WriteLine("\nProduct not found!\n");
                return;
            }

            Console.WriteLine($"Product: {product.Name} - ${product.Price}\n");
            Console.Write("\nAre you sure you want to delete this product? (y/n): ");
            string? confirm = Console.ReadLine()!.ToLower();

            if (confirm == "y")
            {
                _productRepository.Delete(product);
                Console.WriteLine("\n Product deleted successfully!\n");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.\n");
            }
        }

    }
}
