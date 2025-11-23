using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Models;

namespace UserApplication.Repository
{
    public interface IProductRepository
    {
       public void Add(Product product);

        public void Update(Product product);

        public void Delete(Product product);

        public bool IsNameAndIdExist(string name,int id);

        public bool IsNameExist(string name);
        public Product? GetById(int id);

        public IEnumerable<Product> GetAll();

    }
}
