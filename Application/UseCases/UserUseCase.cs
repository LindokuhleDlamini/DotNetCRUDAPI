using System;
using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCases
{
	public class UserUseCase
	{
		private readonly IRepository<User> _userRepository;

		public UserUseCase(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return (List<User>) await _userRepository.GetAllAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateAsync(user.Id, user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}

