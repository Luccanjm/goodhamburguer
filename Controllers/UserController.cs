using GoodHamburguer.Models;
using GoodHamburguer.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburguer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllers : Controller
    {
        private readonly IUsersRepositorie _usersRepositorie;

        public UserControllers(IUsersRepositorie usersRepositorie)
        {
            _usersRepositorie = usersRepositorie;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            List<UserModel> users = await _usersRepositorie.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(Guid id)
        {
            UserModel user = await _usersRepositorie.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Add([FromBody] UserModel userModel)
        {
            UserModel user = await _usersRepositorie.Add(userModel);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel userModel, Guid id)
        {
            userModel.Id = id;
            UserModel user = await _usersRepositorie.Update(userModel, id);
            user = await _usersRepositorie.Update(userModel, id);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(Guid id)
        {
            bool deleted = await _usersRepositorie.Delete(id);

            return Ok(deleted);
        }

    }
}
