using System;
using System.Net;
using System.Web.Http.Results;
using BookStoreAPI.Controllers;
using BookStoreAPI.Models;
using BusinessObjects.Exceptions;
using Moq;
using NUnit.Framework;
using Services;

namespace BookStoreTest.UnitTests
{
    [TestFixture]
    public class BookStoreControllerTest
    {
        const string CATALOGASJSON = @"{
                        'Category':[
                        {
                        'Name': 'Science Fiction', 'Discount': 0.05
                        },
                        {
                        'Name': 'Fantastique', 'Discount': 0.1
                        },
                        {
                        'Name': 'Philosophy', 'Discount': 0.15
                        },
                        ],
                        'Catalog': [
                        {
                        'Name': 'J.K Rowling - Goblet Of fire', 'Category': 'Fantastique',
                        'Price': 8,
                        'Quantity': 2
                        },
                        {
                        'Name': 'Ayn Rand - FountainHead', 'Category': 'Philosophy',
                        'Price': 12,
                        'Quantity': 10
                        },
                        {
                        'Name': 'Isaac Asimov - Foundation', 'Category': 'Science Fiction', 'Price': 16,
                        'Quantity': 1
                        },
                        {
                        'Name': 'Isaac Asimov - Robot series', 'Category': 'Science Fiction', 'Price': 5,
                        'Quantity': 1
                        },
                        {
                        'Name': 'Robin Hobb - Assassin Apprentice', 'Category': 'Fantastique',
                        'Price': 12,
                        'Quantity': 8
                        }
                        ],
                        }";

        private Mock<IBookStoreService> _bookStoreService;

        [SetUp]
        public void SetUp()
        {
            _bookStoreService = new Mock<IBookStoreService>();
        }

        [Test]
        public void BookStoreController_IsInstanceOf_BookStoreController_ReceivingBookStoreServiceOnCreation_Test()
        {
            // Arrange
            var controller = new BookStoreController(_bookStoreService.Object);

            // Act | Assert
            Assert.IsInstanceOf<BookStoreController>(controller);
        }

        [Test]
        public void WhenCallInsertWithCorrectJsonSchema_ReturnsOkResult()
        {
            _bookStoreService.Setup(m => m.ImportStock(CATALOGASJSON)).Returns("import successfully");

            BookStoreController controller = new BookStoreController(_bookStoreService.Object);
            BookStoreModel model = new BookStoreModel();
            model.CatalogAsJson = CATALOGASJSON;

            var result = controller.Import(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<string>), result);
        }

        [Test]
        public void WhenCallImportWithEmptyCatalogAsJson_ReturnsBadRequestErrorMessageResult()
        {
            _bookStoreService.Setup(m => m.ImportStock(CATALOGASJSON)).Returns("import successfully");

            BookStoreController controller = new BookStoreController(_bookStoreService.Object);
            BookStoreModel model = new BookStoreModel();
            model.CatalogAsJson = string.Empty;

            var result = controller.Import(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), result);
        }

        [Test]
        public void WhenCallImportWithWrongJsonShema_ReturnsBadRequestErrorMessageResult()
        {
            _bookStoreService.Setup(m => m.ImportStock(CATALOGASJSON)).Returns("import successfully");

            BookStoreController controller = new BookStoreController(_bookStoreService.Object);
            BookStoreModel model = new BookStoreModel();
            model.CatalogAsJson = "{}";

            var result = controller.Import(model);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), result);
        }

        [Test]
        public void WhenCallGetQuantityWithName_ReturnsOk()
        {
            string name = "name of a book";
            _bookStoreService.Setup(m => m.BookQuantity(name)).Returns(1);
            BookStoreController controller = new BookStoreController(_bookStoreService.Object);

            var result = controller.GetQuantity(name);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<int>), result);

        }

        [Test]
        public void WhenCallGetQuantityWithoutName_ReturnsBadRequestErrorMessageResult()
        {
            string name = string.Empty;
            _bookStoreService.Setup(m => m.BookQuantity(name)).Returns(1);
            BookStoreController controller = new BookStoreController(_bookStoreService.Object);

            var result = controller.GetQuantity(name);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), result);

        }
        [Test]
        public void WhenCallGetPrice_ReturnsOk()
        {
            string[] names = new string[] { "name of a book" };
            _bookStoreService.Setup(m => m.BooksPrice(names)).Returns(10);
            BookStoreController controller = new BookStoreController(_bookStoreService.Object);

            var result = controller.GetPrice(names);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<double>), result);

        }



        [Test]
        public void WhenCallGetPriceThatThrowsNotEnoughInventoryException_ReturnsBadRequestErrorMessageResult()
        {
            string[] names = new string[] { "name of a book" };
            _bookStoreService.Setup(m => m.BooksPrice(names)).Throws(new NotEnoughInventoryException());
            BookStoreController controller = new BookStoreController(_bookStoreService.Object);

            var result = controller.GetPrice(names);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(BadRequestErrorMessageResult), result);

        }
    }
}
