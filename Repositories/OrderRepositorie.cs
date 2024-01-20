using GoodHamburguer.Data;
using GoodHamburguer.Models;
using GoodHamburguer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.Repositories

{
    public class OrderRepositorie : IOrdersRepositorie
    {
        private readonly OrdersDBContext _dbContext;

        public OrderRepositorie(OrdersDBContext ordersDBContext){
            _dbContext = ordersDBContext;
        }

        public async Task<OrderModel> GetOrderById(Guid id)
        {

            return await _dbContext.Orders
                .Include(x => x.User)
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrderModel>> GetAllOrders()
        {
            return await _dbContext.Orders
                .Include(x => x.User)
                .Include(x => x.Products)
                .ToListAsync();
        }

        public async Task<OrderModel> Add(OrderModel order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<OrderModel> Update(OrderModel order, Guid id)
        {
            OrderModel orderById = await GetOrderById(id);
            if (orderById == null)
            {
                throw new Exception($"Order ID: {id} not found.");
            }

            
            orderById.UserId = order.UserId;
            orderById.ProductIds = order.ProductIds;
            orderById.Total = order.Total;

            _dbContext.Orders.Update(orderById);
            await _dbContext.SaveChangesAsync();

            return orderById;
        }

        public async Task<bool> Delete(Guid id)
        {
            OrderModel orderById = await GetOrderById(id);
            if (orderById == null)
            {
                throw new Exception($"Order ID: {id} not found..");
            }

            _dbContext.Orders.Remove(orderById);
            await _dbContext.SaveChangesAsync();

            return true;
        }


    }
}
