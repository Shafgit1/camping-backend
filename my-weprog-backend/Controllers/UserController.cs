using Microsoft.AspNetCore.Mvc;
using my_weprog_backend.Data;
using my_weprog_backend.Models;

namespace my_weprog_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IDataContext _data;

        public UserController(IDataContext data)
        {
            _data = data;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            try
            {
                _data.Register(model);
                return Ok("Registration successful");
            }
            catch (System.Exception ex)
            {
                
                return BadRequest("Unable to register the user.");
            }
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _data.Authenticate(model);
            if (response == null)
                return BadRequest("Username or password is incorrect.");
            return Ok(response);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var users = _data.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _data.GetUserById(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateRequest model)
        {
            try
            {
                _data.UpdateUser(id, model);
                return Ok("User updated successfully");
            }
            catch (System.Exception ex)
            {
              
                return NotFound($"User with ID {id} not found.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _data.DeleteUser(id);
                return Ok("User deleted successfully");
            }
            catch (System.Exception ex)
            {
             
                return NotFound($"User with ID {id} not found.");
            }
        }
    }
}