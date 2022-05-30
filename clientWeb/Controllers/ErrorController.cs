using Microsoft.AspNetCore.Mvc;

namespace ClientWeb.Controllers
{
    public class ErrorController : Controller
    {
        /*  public IActionResult Index()
          {
              return View();
          }*/

        [HttpGet("/error")]
        public IActionResult Error(int? statusCode = null)
        {
                if (statusCode.HasValue)
            {
                this.HttpContext.Response.StatusCode = statusCode.Value;
            }

            ViewData["Error"] = statusCode;
            switch (statusCode)
            {
                case 400:
                    ViewData["Message"] = "Проверьте корректность запроса";
                    break;
                case 403:
                    ViewData["Message"] = "Доступ запрещён!";
                    break;
                case 404:
                    ViewData["Message"] = "Ресурс не найден!";
                    break;
                case 405:
                    ViewData["Message"] = "Метод Htpp не разрешен";
                    break;
                case 500:
                    ViewData["Message"] = "Ошибка сервера";
                    break;
                default:
                    ViewData["Message"] = "Неизвестная ошибка, попробуйте позже";
                    break;
            }
            return View();
        }


        /*
          [HttpGet]        
          public IActionResult Index(int code)
          {
              ViewData["Error"] = code;
              switch (code)
              {
                  case 400:
                      ViewData["Message"] = "Проверьте корректность запроса";
                      break;
                  case 403:
                      ViewData["Message"] = "Доступ запрещён!";
                      break;
                  case 404:
                      ViewData["Message"] = "Ресурс не найден!";
                      break;
                  case 500:
                      ViewData["Message"] = "Ошибка сервера";
                      break;
                  default:
                      ViewData["Message"] = "Неизвестная ошибка, попробуйте позже";
                      break;
              }

              return View();

          }*/
        /*
        [HttpGet]
        [Route("Error/403")]
        public IActionResult Error403()
        {
            ViewData["Error"] = 403;
            ViewData["Message"] = "Доступ запрещён!";
            return View("Error/Index");
        }

        [HttpGet]
        [Route("Error/404")]
        public IActionResult Error404()
        {
            ViewData["Error"] = 404;
            ViewData["Message"] = "Ресурс не найден!";
            return View("Error/Index");
        }

        [HttpGet]
        [Route("Error/500")]
        public IActionResult Error500()
        {
            ViewData["Error"] = 500;
            ViewData["Message"] = "Ошибка сервера";
            return View("Error/Index");
        }
        */
    }
    
}
