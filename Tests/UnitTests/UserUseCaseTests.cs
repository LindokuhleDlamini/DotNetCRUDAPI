using Moq;
using Domain.Entities;
using Application.UseCases;
using Application.Interfaces;


namespace Tests.UnitTests
{
	public class UserUseCaseTests
	{
        private readonly Mock<IRepository<User>> mockUserRepository;
        public UserUseCaseTests()
        {
            mockUserRepository = new Mock<IRepository<User>>();
        }

		[Fact]
		public async Task GetUserById_ShouldReturnOneUser()
		{
            User user = new()
            {
                Id = 1,
                Email = "Test1@email.com",
                Password = "TestPassword1"
            };

            mockUserRepository.Setup(userRepo => userRepo.GetByIdAsync(1)).ReturnsAsync(user);

            UserUseCase userUseCase = new (mockUserRepository.Object);

			var result = await userUseCase.GetUserById(1);

			Assert.Equal(user, result);
		}

        [Fact]
        public async Task GetUsers_ShouldReturnManyUsers()
        {
            User user1 = new()
            {
                Id = 1,
                Email = "Test1@email.com",
                Password = "TestPassword1"
            };

            User user2 = new()
            {
                Id = 2,
                Email = "Test2@email.com",
                Password = "TestPassword2"
            };

            List<User> users = new() { user1, user2  };

            mockUserRepository.Setup(userRepo => userRepo.GetAllAsync()).ReturnsAsync(users);

            var userUseCase = new UserUseCase(mockUserRepository.Object);

            var result = await userUseCase.GetAllUsers();

            Assert.Equal(user1, result[0]);
            Assert.Equal(user2, result[1]);
        }

        [Fact]
        public async Task AddUser_ShouldReturnNewUser()
        {
            User newUser = new()
            {
                Id = 1,
                Email = "Test1@email.com",
                Password = "TestPassword1"
            };


            mockUserRepository.Setup(userRepo => userRepo.AddAsync(newUser)).ReturnsAsync(newUser);

            var userUseCase = new UserUseCase(mockUserRepository.Object);

            var result = await userUseCase.CreateUser(newUser);

            Assert.Equal(newUser, result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnTrue()
        {
            User updatedUser = new()
            {
                Id = 1,
                Email = "Test1@email.com",
                Password = "NewTestPassword1"
            };


            mockUserRepository.Setup(userRepo => userRepo.UpdateAsync(1, updatedUser)).ReturnsAsync(true);

            var userUseCase = new UserUseCase(mockUserRepository.Object);

            var result = await userUseCase.UpdateUser(updatedUser);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnTrue()
        {
            
            mockUserRepository.Setup(userRepo => userRepo.DeleteAsync(1)).ReturnsAsync(true);

            var userUseCase = new UserUseCase(mockUserRepository.Object);

            var result = await userUseCase.DeleteUser(1);

            Assert.True(result);
        }
    }
}

