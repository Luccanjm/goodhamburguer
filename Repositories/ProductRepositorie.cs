using GoodHamburguer.Data;
using GoodHamburguer.Enums;
using GoodHamburguer.Models;
using GoodHamburguer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.Repositories
{
    public class ProductRepositorie : IProductsRepositorie
    {
        private readonly OrdersDBContext _dbContext;
        public ProductRepositorie(OrdersDBContext ordersDBContext) {
            _dbContext = ordersDBContext;
        }
        public async Task<ProductModel> GetProductById(Guid? id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductModel> GetProductByTitle(string title)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Title == title);
        }
        public async Task<List<ProductModel>> GetProductsByType(ProductTypes Type)
        {
            return await _dbContext.Products.Where(p => p.Type == Type).ToListAsync();
        }
        public async Task<List<ProductModel>> GetSandwichesAndExtras()
        {
            return await _dbContext.Products.Where(p => p.Type == ProductTypes.Soda 
                || p.Type == ProductTypes.Fries 
                || p.Type == ProductTypes.Sandwiches)
                .ToListAsync();
        }
        public async Task<List<ProductModel>> GetAllSandwiches()
        {
            return await _dbContext.Products.Where(p => p.Type == ProductTypes.Sandwiches)
                .ToListAsync();
        }
        public async Task<List<ProductModel>> GetAllExtras()
        {
            return await _dbContext.Products.Where(p => p.Type == ProductTypes.Fries 
                || p.Type == ProductTypes.Soda)
                .ToListAsync();
        }
        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<ProductModel> Add(ProductModel product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<ProductModel> Update(ProductModel model, Guid id)
        {
           ProductModel productById = await GetProductById(id);
            if(productById == null)
            {
                throw new Exception($"Produto do ID: {id} não foi encontrado.");
            }
            productById.Title = model.Title;
            productById.Price = model.Price;
            productById.Type = model.Type;

            _dbContext.Products.Update(productById);
            await _dbContext.SaveChangesAsync();

            return productById;
        }

        public async Task<bool> Delete(Guid id)
        {
            ProductModel productById = await GetProductById(id);
            if (productById == null)
            {
                throw new Exception($"Produto do ID: {id} não foi encontrado.");
            }

            _dbContext.Products.Remove(productById);
            await _dbContext.SaveChangesAsync();

            return true;

        }
    }
}
