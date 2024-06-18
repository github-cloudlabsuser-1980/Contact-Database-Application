using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController controller;
        private List<User> userlist;

        [TestInitialize]
        public void Setup()
        {
            controller = new UserController();
            userlist = new List<User>();
        }

        [TestMethod]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            userlist.Add(new User { Id = 2, Name = "Jane", Email = "jane@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userlist, result.Model);
        }

        [TestMethod]
        public void Details_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            userlist.Add(new User { Id = 2, Name = "Jane", Email = "jane@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userlist.FirstOrDefault(u => u.Id == 1), result.Model);
        }

        [TestMethod]
        public void Details_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            userlist.Add(new User { Id = 2, Name = "Jane", Email = "jane@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Details(3) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            userlist.Add(new User { Id = 2, Name = "Jane", Email = "jane@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userlist.FirstOrDefault(u => u.Id == 1), result.Model);
        }

        [TestMethod]
        public void Edit_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            userlist.Add(new User { Id = 2, Name = "Jane", Email = "jane@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Edit(3) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_ValidUser_RedirectsToIndex()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            controller.userlist = userlist;
            var userToUpdate = new User { Id = 1, Name = "Updated John", Email = "updatedjohn@example.com" };

            // Act
            var result = controller.Edit(1, userToUpdate) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Updated John", userlist.FirstOrDefault(u => u.Id == 1).Name);
            Assert.AreEqual("updatedjohn@example.com", userlist.FirstOrDefault(u => u.Id == 1).Email);
        }

        [TestMethod]
        public void Delete_ExistingUserId_ReturnsViewWithUser()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            userlist.Add(new User { Id = 2, Name = "Jane", Email = "jane@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userlist.FirstOrDefault(u => u.Id == 1), result.Model);
        }

        [TestMethod]
        public void Delete_NonExistingUserId_ReturnsHttpNotFound()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            userlist.Add(new User { Id = 2, Name = "Jane", Email = "jane@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Delete(3) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_ValidUser_RedirectsToIndex()
        {
            // Arrange
            userlist.Add(new User { Id = 1, Name = "John", Email = "john@example.com" });
            controller.userlist = userlist;

            // Act
            var result = controller.Delete(1, new FormCollection()) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(userlist.FirstOrDefault(u => u.Id == 1));
        }
    }
}
