using Microsoft.AspNetCore.Mvc;

namespace ClientWeb.Controllers
{
    internal class JavascriptResult : ActionResult
    {
        public string Script { get; set; }
    }
}