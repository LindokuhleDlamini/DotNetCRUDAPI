using System;
using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace Tests.IntegrationTests
{
	public class userControllerTests
	{
		private readonly Mock<UserUseCase> mockUserUseCase;
		public userControllerTests()
		{
			mockUserUseCase = new Mock<UserUseCase>();
		}

		[Fact]
		public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
		{
            User? value = null;
			mockUserUseCase.Setup(options => options.GetUserById(It.IsAny<int>())).ReturnsAsync(value);

			UserController userController = new(mockUserUseCase.Object);

			var result = await userController.GetUser(1);

			Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUser_ReturnsUser_WhenUserIsFound()
        {
            User user = new()
            {
                Id = 1,
                Email = "Test1@email.com",
                Password = "TestPassword1"
            };

            mockUserUseCase.Setup(options => options.GetUserById(It.IsAny<int>())).ReturnsAsync(user);

            UserController userController = new(mockUserUseCase.Object);

            var result = await userController.GetUser(1);

            Assert.IsType<User>(result.Result);
            Assert.Same(user, result.Result);
        }
    }
}

