using Catalog.Api.Data;
using Catalog.Api.Entity;
using MongoDB.Driver;
using System.Linq;
namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;
        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            return await _catalogContext.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(x => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            ReplaceOneResult replaceOneResult = await _catalogContext
                            .Products
                            .ReplaceOneAsync(filter: g => g.Id == product.Id, 
                             replacement: product);
            return replaceOneResult.IsAcknowledged
                  && replaceOneResult.ModifiedCount > 0;
        }
    }
}
