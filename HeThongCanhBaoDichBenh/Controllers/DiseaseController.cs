using HeThongCanhBaoDichBenh.Data;
using HeThongCanhBaoDichBenh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HeThongCanhBaoDichBenh.Controllers
{
    public class DiseaseController : Controller
    {
        private readonly HeThongCanhBaoBenhTomContext _context;
        public DiseaseController(HeThongCanhBaoBenhTomContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;

            var allDiseaseQuery = _context.Diseases
                .Include(d => d.Preventions)
                .Include(d => d.Images);

            var allDiseases = await allDiseaseQuery.ToListAsync();

            PagedList<Disease> lst = new PagedList<Disease>(allDiseases, pageNumber, pageSize);

            return View(lst);
        }

    }
}
