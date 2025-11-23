using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Models;
using UserApplication.Repository;

namespace UserApplication.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context!.Set<Product>().Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Set<Product>().Update(product);
            _context.SaveChanges();

        }

        public bool IsNameAndIdExist(string name, int id)
        {
            return _context.Products.Any(p =>p.Name == name && p.Id != id);
        }

        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool IsNameExist(string name)
        {
            return _context.Products.Any(p => p.Name == name);

        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
         }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
