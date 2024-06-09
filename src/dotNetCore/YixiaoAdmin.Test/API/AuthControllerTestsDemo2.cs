using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.IServices;
using YixiaoAdmin.Models;
using YixiaoAdmin.WebApi.AuthHelper;

namespace YixiaoAdmin.Test.API
{
    [TestClass]
    public class AuthControllerTests
    {
        private AuthController _controller;
        private IUserServices _mockUserServices;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserServices = new Mock<IUserServices>().Object;
            _controller = new AuthController(_mockUserServices);
        }

        [TestMethod]
        public async Task GetJwtStr_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            string name = "user1";
            string pass = "pass1";
            User user = null;
            Mock.Get(_mockUserServices).Setup(x => x.Query(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(new List<User> { user }.AsQueryable()));

            // Act
            var result = await _controller.GetJwtStr(name, pass);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetJwtStr_ReturnsOkResult_WhenUserIsFound()
        {
            // Arrange
            string name = "user1";
            string pass = "pass1";
            User user = new User { Id = "123456", UserName = name, Password = pass, Role = new Role { Name = "admin" } };
            Mock.Get(_mockUserServices).Setup(x => x.Query(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(new List<User> { user }.AsQueryable()));

            // Act
            var result = await _controller.GetJwtStr(name, pass);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var json = JsonConvert.SerializeObject(okResult.Value);
            var value =   JObject.Parse(json);
            Assert.AreEqual(true, value["success"]);
            Assert.AreEqual("admin", value["role"]["Name"]);
        }
    }
}
