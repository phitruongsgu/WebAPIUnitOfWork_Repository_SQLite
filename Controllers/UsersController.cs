using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UOWPocketBookAPI.Core.IConfiguration;
using UOWPocketBookAPI.Models;

namespace UOWPocketBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(ILogger<UsersController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;   
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetUserById", new { user.Id }, user);
            }

            return new JsonResult("Something went wrong!") { StatusCode = 500};
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _unitOfWork.UserRepository.GetAll();
            return Ok(data);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if(id != user.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();   
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var item = await _unitOfWork.UserRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            await _unitOfWork.UserRepository.Delete(item.Id);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}
