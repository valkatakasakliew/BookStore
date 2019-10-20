using BookStore.Data.Interfaces;
using Moq;
using NUnit.Framework;
using Services;
using System;

namespace BookStoreUnitTest
{
    [TestFixture]
    public class BookStoreServicesTest
    {
       
        private Mock<IStore> _store;
        private IBookStoreService _service;

        [SetUp]
        public void SetUp()
        {
            _store = new Mock<IStore>();
            _service = new BookStoreService();
                        
        }

        [Test]
        public void ImportStock_ReturnsResult_Test()
        {
            _store.Setup(x => x.Import("test"));

            var result = _service.ImportStock("test");

            Assert.IsNotNull(result);
        }

        [Test]
        public void ImportStock_ReturnsErroWhenException_Test()
        {
            _store.Setup(x => x.Import("test")).Throws(new Exception());

            var result = _service.ImportStock("test");

            Assert.IsNotNull(result);
            Assert.That(result.ToLower(), Does.Contain("error"));
        }

        [Test]
        public void BookQuantity_ReturnsResult_Test()
        {
            _store.Setup(x => x.Quantity("name")).Returns(It.IsAny<int>);

            var result = _service.BookQuantity("name");

            Assert.IsNotNull(result);
        }

        [Test]
        public void BooksPrice_ReturnsResult_Test()
        {
            string[] names = new string[] { "name" };
            _store.Setup(x => x.Buy( names)).Returns(It.IsAny<double>);

            var result = _service.BooksPrice(names);

            Assert.IsNotNull(result);
        }
    }
}
