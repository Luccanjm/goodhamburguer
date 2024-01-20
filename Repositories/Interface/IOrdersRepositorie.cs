using GoodHamburguer.Models;

namespace GoodHamburguer.Repositories.Interface
{
    public interface IOrdersRepositorie
    {
        Task<List<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrderById(Guid id);
        Task<OrderModel> Add(OrderModel order);
        Task<OrderModel> Update(OrderModel order, Guid id);
        Task<bool> Delete(Guid id);

    }
}
