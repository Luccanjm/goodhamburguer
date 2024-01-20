using GoodHamburguer.Enums;
using GoodHamburguer.Models;

namespace GoodHamburguer.Repositories.Interface
{
    public interface IUsersRepositorie
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(Guid id);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> Add(UserModel model);
        Task<UserModel> Update(UserModel model, Guid id);
        Task<bool> Delete(Guid id);
    }
}