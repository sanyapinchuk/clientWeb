﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClientWeb.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Web.Http;

namespace ClientWeb.Controllers
{
    public class FridgeController : Controller
    {
        // GET: Fridges
        [Route("Fridges")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<mvcFridge> fridgeList;
            HttpResponseMessage response = await GlobalVariables.WebApiClient.GetAsync("Fridge/getAll");
            fridgeList = response.Content.ReadAsAsync<IEnumerable<mvcFridge>>().Result;
            return View(fridgeList);
        }

        // GET: Fridges/{id}
        [HttpGet]
        [Route("Fridges/{id}")]
        public ActionResult GetProducts(int id)
        {
            IEnumerable<mvcProduct> fridgeList = new List<mvcProduct>();
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync($"Fridge/getProducts/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                //fridgeList = response.Content.ReadFromJsonAsync<IEnumerable<(int Item1, string Item2, int Item3)>>().Result;
                fridgeList = response.Content.ReadAsAsync<IEnumerable<mvcProduct>>().Result;
                ViewData["fridgeId"] = id;
                return View(fridgeList);
            }
            else
            {
                // TempData["ErrorMessage"] = "Холодильника с таким id не существует";
                return StatusCode(404);
            }
            
           
        }

        // GET: FridgeController/Create
        [HttpGet]
        [Route("Fridges/Create")]
        public ActionResult CreateFridge()
        {
            IEnumerable<mvcFridgeModel> modelList = new List<mvcFridgeModel>();
            HttpResponseMessage response = GlobalVariables.WebApiClient
                .GetAsync("FridgeModel/getAll").Result;
            modelList = response.Content.ReadAsAsync<IEnumerable<mvcFridgeModel>>().Result;


            IEnumerable<mvcProduct> productList = new List<mvcProduct>();
            HttpResponseMessage response2
                = GlobalVariables.WebApiClient.GetAsync("Product/getAll").Result;
            productList = response2.Content.ReadAsAsync<IEnumerable<mvcProduct>>().Result;
            if (modelList != null)
                ViewData["models"] = modelList;
            if (productList != null)
                ViewData["products"] = productList;
            return View();
        }


         [HttpPost]
         [Route("Fridges/Create")]
         public async Task<ActionResult> CreateFridgeForm(mvcFridge fridge,
                                             int idModel, int[] SelectedProducts)
         {
             fridge.FridgeModelId = idModel;
           //  fridge.FridgeModel = new mvcFridgeModel();
             fridge.Fridge_Products = new List<mvcFridge_Product>();
             //var temp = new { Name = fridge.Name, Owner_name = fridge.Owner_name, FridgeModelId= idModel };
             var json = JsonConvert.SerializeObject(fridge, Formatting.Indented);
             var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
             /* var response
                  = await GlobalVariables.WebApiClient.PostAsync("Fridge/addFridge"+
                  "?Name="+fridge.Name+"&Owner_name="+fridge.Owner_name+"&FridgeModelId="
                  +idModel.ToString(), null);
             */
             var response
                  = await GlobalVariables.WebApiClient.PostAsync("Fridge/addFridge", stringContent);
             if (response.IsSuccessStatusCode) 
             {
                var idStr = await response.Content.ReadAsStringAsync();                

                if (int.TryParse(idStr, out int id))
                {
                    foreach (var product in SelectedProducts)
                    {
                        mvcFridge_Product fr_pr = new();

                        fr_pr.FridgeId = id;
                        fr_pr.ProductId = product;
                        fr_pr.Quantity = 0;     //set deafault quantity value
                        var json2 = JsonConvert.SerializeObject(fr_pr, Formatting.Indented);
                        var stringContent2 = new StringContent(json2, Encoding.UTF8, "application/json");
                        var response2
                        = await GlobalVariables.WebApiClient.PostAsync("Fridge/addProduct", stringContent2);
                        if (!response2.IsSuccessStatusCode)
                        {
                            await Response.WriteAsync("<script>alert('Не все продукты были добавлены')</script>", Encoding.Unicode);
                            break;
                        }
                    }
                        
                    return RedirectToAction("Index");
                }
                else
                {
                    await Response.WriteAsync("<script language=javascript>alert('Продукты были добавлены')</script>", Encoding.Unicode);
                    return RedirectToAction("Index");
                }
                
             }
             else
             {
                 await Response.WriteAsync("<script language=javascript>alert('Холодильник не был добавлен')</script>", Encoding.Unicode);
                 return Ok();
             }
         }
         

        // GET: FridgeController/Edit/5
        [HttpGet]
        [Route("Fridges/Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await GlobalVariables.WebApiClient.GetAsync($"Fridge/getFridge/{id}");
            if (response.IsSuccessStatusCode)
            {
                mvcFridge fridge = await response.Content.ReadAsAsync<mvcFridge>();

                IEnumerable<mvcFridgeModel> modelList = new List<mvcFridgeModel>();
                HttpResponseMessage response2 = GlobalVariables.WebApiClient
                    .GetAsync("FridgeModel/getAll").Result;
                modelList = response2.Content.ReadAsAsync<IEnumerable<mvcFridgeModel>>().Result;

                if(modelList!=null)
                    ViewData["models"] = modelList;

                return View(fridge);
            }
            else
                return StatusCode(400);
            
        }

        [HttpPost]
        [Route("Fridges/Edit")]
        public async Task<ActionResult> EditPost(int fridgeId, string Name, string Owner_name, int FridgeModelId)
        {
            mvcFridge fridge = new()
            {
                Id = fridgeId,
                Name = Name,
                Owner_name = Owner_name,
                FridgeModelId = FridgeModelId
            };
            var json = JsonConvert.SerializeObject(fridge, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await GlobalVariables.WebApiClient.PutAsync($"Fridge/edit/{fridgeId}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
              //  return StatusCode(404);
                // await Response.WriteAsync("<script language=javascript>alert('Холодильник не был обналвен')</script>", Encoding.Unicode);
                return StatusCode(((int)response.StatusCode));
               // throw new System.Web.Http.HttpResponseException(response.StatusCode);
            }
        }


        [HttpDelete]
        [Route("Fridges/Delete/{id}")]
        public ActionResult Delete(int id)
        {
              var response
                 =  GlobalVariables.WebApiClient.DeleteAsync($"Fridge/removeFridge/{id}").Result;

            if (!response.IsSuccessStatusCode)
            {
                Response.WriteAsync("<script>alert(\" Холодильник не был удален \")</script>");
                return StatusCode(400, "Ошибка удлаения");
            }
            else
            {                
                return StatusCode(200, "Удалено");
            }

        }

        
        [HttpPost]
        [Route("Fridges/ChangeProducts")]
        public async Task<ActionResult> ChangeCountProducts(int fridgeId, int[] productsId, int[] productCount)
        {
            var firstResponse =  await GlobalVariables.WebApiClient.GetAsync($"Fridge/getProducts/{fridgeId}");
           if (firstResponse.IsSuccessStatusCode)
            {
                var productsDb = await firstResponse.Content.ReadAsAsync<IList<mvcProduct>>();
                for(int i = 0; i < productsId.Length; i++)
                {
                    if(productCount[i] != productsDb[i].Default_quantity)
                    {
                        var fr_pr = new mvcFridge_Product()
                        {
                            FridgeId = fridgeId,
                            ProductId = productsId[i],
                            Quantity = productCount[i]
                        };

                        var json = JsonConvert.SerializeObject(fr_pr, Formatting.Indented);

                        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var secResponse = await GlobalVariables.WebApiClient.PutAsync("Fridge/changeCountProducts", stringContent);
                        if (!secResponse.IsSuccessStatusCode)
                        {
                            return StatusCode((int)secResponse.StatusCode);
                        }
                    }

                }
                Response.StatusCode = 200;
                return RedirectToAction($"Index");
            }
            else
                return StatusCode((int)firstResponse.StatusCode);
        }
    }
}
