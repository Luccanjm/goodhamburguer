using GoodHamburguer.Enums;
using GoodHamburguer.Models;
using GoodHamburguer.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburguer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrdersRepositorie _ordersRepositorie;
        private readonly IProductsRepositorie _productsRepositorie;
        private readonly IUsersRepositorie _usersRepositorie;

        public OrderController(IOrdersRepositorie ordersRepositorie, IProductsRepositorie productsRepositorie, IUsersRepositorie usersRepositorie)
        {
            _ordersRepositorie = ordersRepositorie;
            _productsRepositorie = productsRepositorie;
            _usersRepositorie = usersRepositorie;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderModel>>> GetAllOrders()
        {
            List<OrderModel> orders = await _ordersRepositorie.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrderById(Guid id)
        {
            OrderModel order = await _ordersRepositorie.GetOrderById(id);
            return Ok(order);

        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> Add([FromBody] OrderModel orderModel)
        {
            
            if(orderModel.ProductIds != null && orderModel.ProductIds.Count > 0)
            {
                try {
                    var result = await CheckSameTypeRule(orderModel);
                    
                
                // Check if there are products with the same type in the order
                if (result.SameType)
                {
                    Console.WriteLine("Duplicate types detected in the order!");

                    return Conflict("An order can not have products with the same type.");
                }
                else
                {
                    await ProcessOrder(orderModel);
                }
                

                OrderModel order = await _ordersRepositorie.Add(orderModel);


                    return Ok($"The order total is: {order.Total}");
            
                }  
                catch{
                    return StatusCode(500, "An error occurred while processing the request.");
                }
        }
            else 
            {  
                return BadRequest("No products added to order. ProductIds are null!"); 
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderModel>> Update([FromBody] OrderModel orderModel, Guid id)
        {
            if (orderModel.ProductIds != null && orderModel.ProductIds.Count > 0)
            {
                try
                {

                    var result = await CheckSameTypeRule(orderModel);

                    // Check if there are products with the same type in the order
                    if (result.SameType)
                    {
                        Console.WriteLine("Duplicate types detected in the order!");

                        return Conflict("An order can not have products with the same type.");
                    }
                    else
                    {
                        await ProcessOrder(orderModel);
                    }

                   
                    orderModel.Id = id;
                    OrderModel order = await _ordersRepositorie.Update(orderModel, id);


                    return Ok($"The order total is: {order.Total}");

                }
                catch
                {
                    return StatusCode(500, "An error occurred while processing the request.");
                }
            }
            else
            {
                return BadRequest("No products added to order. ProductIds are null!");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderModel>> Delete(Guid id)
        {
            bool deleted = await _ordersRepositorie.Delete(id);

            return Ok(deleted);
        }

        private async Task<(bool SameType, List<ProductModel> Items)> CheckSameTypeRule(OrderModel orderModel)
        {
            var items = new List<ProductModel>();
            foreach (Guid id in orderModel.ProductIds)
                items.Add(await _productsRepositorie.GetProductById(id));

            var sandwiches = items.Where(product => product.Type == ProductTypes.Sandwiches).Any();
            var soda = items.Where(product => product.Type == ProductTypes.Soda).Any();
            var fries = items.Where(product => product.Type == ProductTypes.Fries).Any();
            bool sameType = items.GroupBy(product => product.Type).Any(group => group.Count() > 1);
            return (sameType, items);
        }

        private async Task ProcessOrder(OrderModel order)
        {
            // Total Calculation
            decimal totalAmount = await CalculateTotalAmountAsync(order);

            // Set the total amount
            order.Total = totalAmount;

        }

        private async Task<decimal> CalculateTotalAmountAsync(OrderModel order)
{
            decimal totalAmount = 0.0m;

            var items = await GetProductsByIds(order.ProductIds);
            totalAmount = items.Sum(item => item.Price);
            Console.WriteLine(totalAmount); 
            var sandwiches = items.Any(product => product.Type == ProductTypes.Sandwiches);
            var soda = items.Any(product => product.Type == ProductTypes.Soda);
            var fries = items.Any(product => product.Type == ProductTypes.Fries);

            // Apply discounts based on selected items
            if (sandwiches && soda && fries)
            {
                order.Total = totalAmount * 0.8m; // 20% discount for sandwich, fries, and soda
            }
            else if (sandwiches && soda)
            {
                order.Total = totalAmount * 0.85m; // 15% discount for sandwich and soda
            }
            else if (sandwiches && fries)
            {
                order.Total = totalAmount * 0.9m; // 10% discount for sandwich and fries
            }else
            {
                order.Total = totalAmount;
            }

            return order.Total;
}

        private async Task<List<ProductModel>> GetProductsByIds(List<Guid?>? productIds)
        {
            var products = new List<ProductModel>();
            foreach (Guid id in productIds)
            {
                var product = await _productsRepositorie.GetProductById(id);
                products.Add(product);
            }
            return products;
        }
    }
}
