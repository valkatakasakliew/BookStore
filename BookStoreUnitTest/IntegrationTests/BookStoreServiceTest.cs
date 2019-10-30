using System;
using System.Net;
using System.Web.Http.Results;
using BookStoreAPI.Controllers;
using BookStoreAPI.Helpers;
using BookStoreAPI.Models;
using BusinessObjects.Exceptions;
using Moq;
using NUnit.Framework;
using Services;

namespace BookStoreTest.IntegrationTests
{
    [TestFixture]
    public class BookStoreServiceTest
    {
        private IBookStoreService _bookStoreService;

        [SetUp]
        public void SetUp()
        {
            _bookStoreService = new BookStoreService();
            BookStoreService_DoesNotThrowException_WhenImportCatalog();
        }

        [Test]
        public void BookStoreService_DoesNotThrowException_WhenImportCatalog()
        {
             Assert.DoesNotThrow(() => _bookStoreService.ImportStock(Constants.EXAMPLE_CATALOG));
        }


        [TestCase("J.K Rowling - Goblet Of fire", 2)]
        [TestCase("Ayn Rand - FountainHead", 10)]
        [TestCase("Isaac Asimov - Foundation", 1)]
        [TestCase("Isaac Asimov - Robot series", 1)]
        [TestCase("Robin Hobb - Assassin Apprentice", 8)]

        public void BookStoreService__WhenCheckQuantityForBookTitle_ReturnsExpectedResult(string bookTitle, int expectedResult)
        {

            int result = _bookStoreService.BookQuantity(bookTitle);

            Assert.IsTrue(result == expectedResult);
        }

        [TestCase(new string[] { "J.K Rowling - Goblet Of fire" , "Robin Hobb - Assassin Apprentice" }, 18.0)]
        [TestCase(new string[] { "J.K Rowling - Goblet Of fire", "Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice" }, 30.0)]
        [TestCase(new string[] { "Ayn Rand - FountainHead", "Isaac Asimov - Robot series", "Isaac Asimov - Foundation","J.K Rowling - Goblet Of fire",
            "J.K Rowling - Goblet Of fire","Robin Hobb - Assassin Apprentice", "Robin Hobb - Assassin Apprentice" }, 69.95)]
        public void BookStoreService_WhenBuy_ReturnsExpectedresult(string[] books,double expectedResult)
        {
            double result = _bookStoreService.BooksPrice(books);

            Assert.IsTrue(result == expectedResult);
        }

        [TestCase(new string[] { "Isaac Asimov - Foundation", "Isaac Asimov - Foundation" },0)]
        public void BookStoreService_WhenBuyMoreQuantityThanExist_ReturnsNotEnoughInventoryException(string[] books,double expectedResult)
        {
             Assert.Throws<NotEnoughInventoryException>(() => _bookStoreService.BooksPrice(books));
        }

    }
}
