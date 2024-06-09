using Microsoft.VisualStudio.TestTools.UnitTesting;
using YixiaoAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YixiaoAdmin.IRepository;
using Microsoft.EntityFrameworkCore;
using YixiaoAdmin.EntityFrameworkCore;
using YixiaoAdmin.IServices;
using System.Net;
using YixiaoAdmin.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace YixiaoAdmin.Tests.API
{
    [TestClass]
    public class UnitTest1
    {
        private HttpClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            // Arrange
            var name = "admin";
            var password = "123456";
            var expectedToken = "token";

            // Act
            var response = await _client.GetAsync($"/Auth?name={name}&pass={password}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(responseString.Contains(expectedToken));
        }
    }
}