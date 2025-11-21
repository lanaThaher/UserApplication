using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Models;


namespace UserApplication
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService()
        {
            _context = new ApplicationDbContext();
        }

        public void AddProduct()
        {
            using var _context = new ApplicationDbContext();

            Console.Write("Enter name: ");
            var name = Console.ReadLine();

            Console.Write("Enter price: ");
            var priceInput = Console.ReadLine();

            if (name == "" || priceInput == "")
            {
                Console.WriteLine("\nError: Name and price are required!\n");
                return;
            }

            if (!double.TryParse(priceInput, out double price))
            {
                Console.WriteLine("\nError: Price must be a valid number!\n");
                return;
            }


            bool IsNameExist = _context.Products.Any(n => n.Name == name);
            if (IsNameExist)
            {
                Console.WriteLine("\nError: a product with this name already exists\n");
            }
            else
            {
                var product = new Product { Name = name, Price = price };
                _context.Products.Add(product);
                _context.SaveChanges();

                Console.WriteLine("\nProduct added successfully\n");
            }
        }

        public void UpdateProduct()
        {
            using var _context = new ApplicationDbContext();

            Console.WriteLine("\nEnter the ID of the product to update it \n");
            string idInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Error: Invalid ID!\n");
                return;
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Product not found!\n");
                return;
            }

            Console.WriteLine($"The Name of the product is {product.Name}");
            Console.WriteLine($"The price of the product is {product.Price}");

            Console.Write("Enter new name (leave empty to keep current): ");
            string newName = Console.ReadLine();
            if (newName != "")
            {
                bool nameExists = _context.Products.Any(p => p.Name == newName && p.Id != id);
                if (nameExists)
                {
                    Console.WriteLine("Error: a product with this name already exists!\n");
                    return;
                }
                product.Name = newName;
            }

            Console.Write("Enter new price (leave empty to keep current): ");
            string newPriceInput = Console.ReadLine();
            if (newPriceInput != "")
            {
                if (!double.TryParse(newPriceInput, out double newPrice))
                {
                    Console.WriteLine("Error: Price must be a valid number!\n");
                    return;
                }
                product.Price = newPrice;
            }


            _context.SaveChanges();
            Console.WriteLine("Product updated successfully\n");
        }

        public void getProductDetails()
        {
            using var _context = new ApplicationDbContext();

            Console.WriteLine("Enter the ID of the product to View the details ");
            int id = Convert.ToInt32(Console.ReadLine());

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

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
            using var _context = new ApplicationDbContext();

            var productList = _context.Products.ToList();

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


        public void deleteProduct()
        {
            using var _context = new ApplicationDbContext();

            Console.Write("Enter product ID to delete: ");
            string idInput = Console.ReadLine();

            if (idInput == "")
            {
                Console.WriteLine("\nError: ID is required!\n");
                return;
            }

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("\nError: Invalid ID!\n");
                return;
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("\nProduct not found!\n");
                return;
            }

            Console.WriteLine($"Product: {product.Name} - ${product.Price}\n");
            Console.Write("\nAre you sure you want to delete this product? (y/n): ");
            string confirm = Console.ReadLine().ToLower();

            if (confirm == "y")
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                Console.WriteLine("\n Product deleted successfully!\n");
            }
            else
            {
                Console.WriteLine("\nOperation cancelled.\n");
            }
        }

    }
}
