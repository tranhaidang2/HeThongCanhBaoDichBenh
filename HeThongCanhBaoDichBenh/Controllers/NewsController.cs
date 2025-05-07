using HeThongCanhBaoDichBenh.Data;
using HeThongCanhBaoDichBenh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HeThongCanhBaoDichBenh.Controllers
{
    public class NewsController : Controller
    {
        private readonly HeThongCanhBaoBenhTomContext _context;
        public NewsController(HeThongCanhBaoBenhTomContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;

            var allNewsQuery = _context.News
                .Include(n => n.User)
                .Include(n => n.Images)
                .OrderByDescending(n => n.NewsCreateAt);

            var allNews = await allNewsQuery.ToListAsync();
            PagedList<News> lst = new PagedList<News>(allNews, pageNumber, pageSize);

            return View(lst);
        }

    }
}
