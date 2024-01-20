using GoodHamburguer.Enums;
using GoodHamburguer.Models;

namespace GoodHamburguer.Repositories.Interface
{
    public interface IProductsRepositorie
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(Guid? id);
        Task<ProductModel> GetProductByTitle(string title);
        Task<List<ProductModel>> GetProductsByType(ProductTypes Type);
        Task<List<ProductModel>> GetAllSandwiches();
        Task<List<ProductModel>> GetSandwichesAndExtras();
        Task<List<ProductModel>> GetAllExtras(); 

        Task<ProductModel> Add(ProductModel model);
        Task<ProductModel> Update(ProductModel model, Guid id);
        Task<bool> Delete(Guid id);
    }
}
