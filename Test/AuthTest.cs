using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using route_Tsak.Controllers;
using route_Tsak.Dto;
using route_Tsak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class AuthTest
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;

        public AuthTest()
        {
            _mockUserManager = MockUserManager<ApplicationUser>();
            _mockRoleManager = MockRoleManager();
        }

        [Fact]
        public async Task Register_ShouldRegisterUser()
        {
            // Arrange
            var authController = new AuthController(_mockUserManager.Object, _mockRoleManager.Object);
            var newUserDto = new NewUserDto
            {
                Name = "TestUser",
                Email = "testuser@example.com",
                Address = "Test Address",
                Password = "TestPassword123"
            };

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockRoleManager.Setup(rm => rm.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockRoleManager.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await authController.Register(newUserDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User Registered Successfully", ((dynamic)okResult.Value).Message);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var authController = new AuthController(_mockUserManager.Object, _mockRoleManager.Object);
            var loginModel = new LoginModel
            {
                Email = "testuser@example.com",
                Password = "TestPassword123"
            };

            var user = new ApplicationUser { UserName = "TestUser", Email = "testuser@example.com", Id = "1" };

            _mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(um => um.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);
            _mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> { "Customer" });

            // Act
            var result = await authController.Login(loginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(((dynamic)okResult.Value).token);
        }

        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(
                store.Object, null, null, null, null, null, null, null, null);
        }

        private Mock<RoleManager<IdentityRole>> MockRoleManager()
        {
            var store = new Mock<IRoleStore<IdentityRole>>();
            return new Mock<RoleManager<IdentityRole>>(
                store.Object, null, null, null, null);
        }
    }
}

