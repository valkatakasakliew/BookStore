
using BookStoreAPI.Helpers;
using BookStoreAPI.Models;
using BusinessObjects.Exceptions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Services;
using System.Web.Http;

namespace BookStoreAPI.Controllers
{

    public class BookStoreController : ApiController
    {
        private readonly IBookStoreService _bookStoreService;

        public BookStoreController(IBookStoreService bookStoreService)
        {
            _bookStoreService = bookStoreService;
        }

        [HttpPost]
        [Route("api/BookStore/importStock")]
        public IHttpActionResult Import([FromBody]BookStoreModel model)
        {
            string catalogAsJson = model.CatalogAsJson;

            if (string.IsNullOrEmpty(catalogAsJson))
                return BadRequest("Not provided catalog json");

            JSchema jsonSchema = JSchema.Parse(Constants.JSON_SCHEMA);
            JObject jsonObject = JObject.Parse(catalogAsJson);

            if (!jsonObject.IsValid(jsonSchema))
                return BadRequest("Invalid data");
            else
                return Ok(_bookStoreService.ImportStock(catalogAsJson));
        }

        [HttpGet]
        [ActionName("getQuantityByName")]
        public IHttpActionResult GetQuantity([FromUri] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Book name is not provided");
            else
                return Ok (_bookStoreService.BookQuantity(name)) ;
        }

        [HttpGet]
        [ActionName("getBooksPrice")]
        public IHttpActionResult GetPrice([FromUri] string[] bookNames)
        {
            double result = 0;
            try
            {
                result = _bookStoreService.BooksPrice(bookNames);
            }
            catch (NotEnoughInventoryException neix)
            {
                return BadRequest($"Invalid shopping cart.Could not calculate price.See errors below.{neix.Message}");
            }
            return Ok(result);
        }

    }
}