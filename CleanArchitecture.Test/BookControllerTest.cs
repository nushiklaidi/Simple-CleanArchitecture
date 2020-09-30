using CleanArchitecture.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CleanArchitecture.Test
{
    [TestClass]
    public class BookControllerTest
    {
        private readonly BookController _controller;
        private readonly BookServiceFake _service;

        public BookControllerTest()
        {
            _service = new BookServiceFake();
            _controller = new BookController(_service);
        }

        [TestMethod]
        public void Get_AllBook()
        {
            //Arrange
            var countItem = 2;

            //Act
            var result = _controller.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(countItem, result.Value.Count());
        }

        [TestMethod]
        public void Get_BookById()
        {
            //Arrange
            var id = 11;
            //Act
            var result = _controller.BookDetail(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Value.Id);
        }
    }
}
