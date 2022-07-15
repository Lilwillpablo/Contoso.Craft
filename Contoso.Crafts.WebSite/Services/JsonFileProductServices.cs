using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Contoso.Crafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFileProductService
    {   //get json file path path and deseriaalize it
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "product.json"); }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        //add new 5 strars rating sub-service 
        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            //LINQ
            var query = products.First(x => x.Id == productId); 

            if(query.Raitings == null)
            {
                query.Raitings = new int[] { rating };

            }
            else
            {
                //add to list for our convinience and switch back to rhe array
                var ratings = query.Raitings.ToList();
                ratings.Add(rating);
                query.Raitings = ratings.ToArray();
            }
            //add to the JSONfile rating wich was added by API like https://localhost:7183/products/rate?ProductId=jenlooper-cactus&Rating=5  
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true,
                    }),
                    products
                );
            }

        }
    }
}