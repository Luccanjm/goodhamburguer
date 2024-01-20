using GoodHamburguer.Enums;
using GoodHamburguer.Models;
using GoodHamburguer.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburguer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductsRepositorie _productsRepositorie;

        public ProductController(IProductsRepositorie productsRepositorie)
        {
            _productsRepositorie = productsRepositorie;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetAllProducts()
        {
            List<ProductModel> products = await _productsRepositorie.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("/sandwiches&extras")]
        public async Task<ActionResult<List<ProductModel>>> GetSandwichesAndExtras()
        {
            List<ProductModel> products = await _productsRepositorie.GetSandwichesAndExtras();
            return Ok(products);
        }

        [HttpGet("/sandwiches")]
        public async Task<ActionResult<List<ProductModel>>> GetAllSandwiches()
        {
            List<ProductModel> products = await _productsRepositorie.GetAllSandwiches();
            return Ok(products);
        }

        [HttpGet("/extras")]
        public async Task<ActionResult<List<ProductModel>>> GetAllExtras()
        {
            List<ProductModel> products = await _productsRepositorie.GetAllExtras();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Add([FromBody] ProductModel productModel)
        {
            ProductModel product = await _productsRepositorie.Add(productModel);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductModel>> Update([FromBody] ProductModel productModel, Guid id)
        {
            productModel.Id = id;
            ProductModel product = await _productsRepositorie.Update(productModel, id);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductModel>> Delete(Guid id)
        {
            bool deleted = await _productsRepositorie.Delete(id);

            return Ok(deleted);
        }


    }
}
