
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Chat(int idChat)
        {
            return View(idChat);
        }
    }
}
