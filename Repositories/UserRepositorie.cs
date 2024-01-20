using GoodHamburguer.Models;
using GoodHamburguer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using GoodHamburguer.Data;

namespace GoodHamburguer.Repositories
{
    public class UserRepositorie : IUsersRepositorie
    {
        private readonly OrdersDBContext _dbContext;
        public UserRepositorie(OrdersDBContext ordersDBContext)
        {
            _dbContext = ordersDBContext;
        }
        public async Task<UserModel> GetUserById(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<UserModel> GetUserByName(string name)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> Add(UserModel model)
        {
            await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<UserModel> Update(UserModel model, Guid id)
        {
            UserModel userById = await GetUserById(id);
            if (userById == null)
            {
                throw new Exception($"Usuário do ID: {id} não foi encontrado.");
            }
            userById.Name = model.Name;
            userById.Email = model.Email;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> Delete(Guid id)
        {
            UserModel userById = await GetUserById(id);
            if (userById == null)
            {
                throw new Exception($"Usuário do ID: {id} não foi encontrado.");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
