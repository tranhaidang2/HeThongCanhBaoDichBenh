using HeThongCanhBaoDichBenh.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeThongCanhBaoDichBenh.Controllers
{
    public class NewsController : Controller
    {
        private readonly HeThongCanhBaoBenhTomContext _context;
        public NewsController(HeThongCanhBaoBenhTomContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var allNews = _context.News
                .Include(n => n.User)
                .Include(n => n.Images)
                .OrderByDescending(n => n.NewsCreateAt)
                .ToList();
            return View(allNews);
        }
    }
}
