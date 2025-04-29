using HeThongCanhBaoDichBenh.Data;
using Microsoft.AspNetCore.Mvc;

namespace HeThongCanhBaoDichBenh.Controllers
{
    public class AccountController : Controller
    {
        private readonly HeThongCanhBaoBenhTomContext _context;
        public AccountController(HeThongCanhBaoBenhTomContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

    }
}
