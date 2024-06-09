using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YixiaoAdmin.IRepository;
using YixiaoAdmin.Models;

namespace YourProjectName.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private UserServices _userService;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository
                .Setup(repo => repo.Query(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>
                {
                    new User
                    {
                        UserName = "username",
                        Password = "password",
                        Role = new Role
                        {
                            Name = "admin"
                        }
                    }
                }.AsQueryable());

            _userService = new UserServices(mockUserRepository.Object);
        }

        [TestMethod]
        public async Task Login_WithValidCredentials_ShouldReturnSuccessResponse()
        {
            // Act
            var result = await _userService.Login("username", "password");

            // Assert
            Assert.IsTrue(result.Code==200);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOfType(result.Data, typeof(User));
            Assert.AreEqual("username", result.Data.UserName);
            Assert.AreEqual("password", result.Data.Password);
            Assert.AreEqual("admin", result.Data.Role.Name);
        }

        [TestMethod]
        public async Task Login_WithInvalidCredentials_ShouldReturnNotFoundResponse()
        {
            // Act
            var result = await _userService.Login("invalidUsername", "invalidPassword");

            // Assert
            Assert.IsFalse(result.Code == 200);
            Assert.IsNull(result.Data);
        }
    }
}

