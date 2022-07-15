using Contoso.Crafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Crafts.WebSite.Controllers
{
    //Add controle for simple API to possabilty to see entire JSON file if i put "/products" to url 
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {

        public ProductsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }

        //Controler for rating
        [Route("Rate")] // add  Rate to the rout => https://localhost:7183/products/rate?ProductId=jenlooper-cactus&Rating=5
        [HttpGet]
        public ActionResult Get(
            [FromQuery]string ProductId,
            [FromQuery] int Rating) 
        {
            ProductService.AddRating(ProductId, Rating);
            return Ok();
        
        
        }

    }

}
