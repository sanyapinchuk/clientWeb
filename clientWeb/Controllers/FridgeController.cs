using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClientWeb.Models;

namespace ClientWeb.Controllers
{
    public class FridgeController : Controller
    {
        // GET: Fridges
        [Route("Fridges")]
        public ActionResult Index()
        {
            IEnumerable<mvcFridge> fridgeList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Fridge/getAll").Result;
            fridgeList = response.Content.ReadAsAsync<IEnumerable<mvcFridge>>().Result;
            return View(fridgeList);
        }

        // GET: Fridges/{id}
        [Route("Fridges/{id}")]
        public ActionResult GetProducts(int id)
        {
            IEnumerable<mvcProduct> fridgeList = new List<mvcProduct>();
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync($"Fridge/getProducts/{id}").Result;
            if(response.IsSuccessStatusCode)
            {
                //fridgeList = response.Content.ReadFromJsonAsync<IEnumerable<(int Item1, string Item2, int Item3)>>().Result;
                fridgeList = response.Content.ReadAsAsync<IEnumerable<mvcProduct>>().Result;                
            }
            TempData["ErrorMessage"] = "Холодильника с таким id не существует";
            return View(fridgeList);
        }

        // GET: FridgeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FridgeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FridgeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FridgeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FridgeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FridgeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
