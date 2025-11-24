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

        public string AddProduct(string name , string price)
        {

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price))
            {
                return "\nError: Name and price are required!\n";
            }

            if (!double.TryParse(price, out double priceDouble))
            {
                return "\nError: Price must be a valid number!\n";
                
            }

            if (_productRepository.IsNameExist(name))
            {
                return "\nError: a product with this name already exists\n";
            }
            else
            {
                var product = new Product { Name = name, Price = priceDouble };
                _productRepository.Add(product);

                return "\nProduct added successfully\n";
            }
        }

        public string UpdateProduct(int id, string newName, string newPrice)
        {
            var product = _productRepository.GetById(id);

            if (!string.IsNullOrEmpty(newName))
            {
                bool nameExists = _productRepository.IsNameAndIdExist(newName, id);
                if (nameExists)
             
                    return "Error: a product with this name already exists!\n";

                
                product!.Name = newName;
            }

            if (!string.IsNullOrEmpty(newPrice))
            {
                if (!double.TryParse(newPrice, out double Price))
                
                    return "Error: Price must be a valid number!\n";

                    product!.Price = Price;
                

            }

            _productRepository.Update(product!);
            return "Product updated successfully\n";
        }

        public Product? getProductDetails(int id )
        {

            return _productRepository.GetById(id);

        }

        public IEnumerable<Product> listAllProduct()
        {

            var productList = _productRepository.GetAll();
            return productList;


           
        }


        public string DeleteProduct(int id )
        {

            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return "\nProduct not found!\n";
                
            }
            _productRepository.Delete(product);
             return "\n Product deleted successfully!\n";

        }

    }
}
