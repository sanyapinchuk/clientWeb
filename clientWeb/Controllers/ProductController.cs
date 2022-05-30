using ClientWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ClientWeb.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("Products")]
        public async Task<ActionResult> Index()
        {
            var response = await GlobalVariables.WebApiClient.GetAsync("Product/getAll");
            if(response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsAsync<IEnumerable<mvcProduct>>();
                return View(products);
            }
            return StatusCode(500);
        }

        [HttpGet]
        [Route("Product/Edit/{id}")]
        public async Task<ActionResult> EditProduct(int id)
        {
            var response = await GlobalVariables.WebApiClient.GetAsync($"Product/{id}");
            var product = await response.Content.ReadAsAsync<mvcProduct>();
            return View(product);
        }

        [HttpPost]
        [Route("Product/Edit")]
        public async Task<ActionResult> EditProductForm(int idProduct, string Name, int Default_quantity)
        {
            var product = new mvcProduct()
            {
                Name = Name,
                Default_quantity = Default_quantity
            };
            var json = JsonConvert.SerializeObject(product, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await GlobalVariables.WebApiClient.PutAsync($"Product/Edit/{idProduct}", stringContent); 
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return StatusCode((int)response.StatusCode);
        }

        
    }
}
