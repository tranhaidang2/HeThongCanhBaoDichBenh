using HeThongCanhBaoDichBenh.Data;
using Microsoft.AspNetCore.Mvc;

namespace HeThongCanhBaoDichBenh.Controllers
{
    public class ForumController : Controller
    {
        private readonly HeThongCanhBaoBenhTomContext _context;
        public ForumController(HeThongCanhBaoBenhTomContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
