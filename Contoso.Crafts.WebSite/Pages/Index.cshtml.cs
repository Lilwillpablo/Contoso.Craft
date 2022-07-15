using Contoso.Crafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Contoso.Crafts.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        // add our 
        public JsonFileProductService ProductService;
        // add our 
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService) // add our 

        {
            _logger = logger;
            ProductService = productService;// add our 

        }

        public void OnGet()
        {
            Products = ProductService.GetProducts(); // add our 

        }
    }
}