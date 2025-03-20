using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
	{
		private readonly UserUseCase _userUseCase;
		public UserController(UserUseCase userUseCase)
		{
			_userUseCase = userUseCase;
		}

		[HttpGet("id")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var user = await _userUseCase.GetUserById(id);

			if (user == null)
			{
				return new NotFoundResult();
			}
			return user;
		}

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
			return await _userUseCase.GetAllUsers();
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            return await _userUseCase.CreateUser(user);
        }

        [HttpPut("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            return await _userUseCase.DeleteUser(id);
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> UpdateUser(User user)
        {
            return await _userUseCase.UpdateUser(user);
        }


    }
}

