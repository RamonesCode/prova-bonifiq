using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService
    {

        private readonly TestDbContext _testDbContext;

        public ProductService(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public ProductList ListProducts(int page)
        {
            int pageSize = 10; //10 produtos por pag.
            int totalCount = _testDbContext.Products.Count(); // verifica qntd total de itens
            int skip = (page - 1) * pageSize;
            List<Product> products = _testDbContext.Products.Skip(skip).Take(pageSize).ToList();
            bool hasNext = skip + pageSize <= totalCount;

            return new ProductList() { HasNext = hasNext, TotalCount = totalCount, Items = products };
        }

    }
}
