using HeThongCanhBaoBenhTom.Helper;
using HeThongCanhBaoDichBenh.Data;
using HeThongCanhBaoDichBenh.Models;
using HeThongCanhBaoDichBenh.ViewModels;
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
        [HttpPost]
        public JsonResult Register([FromBody] RegisterVM model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return Json(new { message = "Vui lòng nhập đầy đủ thông tin." });
            }

            if (_context.Users.Any(u => u.Username == model.Username))
            {
                return Json(new { message = "Tên đăng nhập đã tồn tại." });
            }

            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = model.Username,
                PasswordHash = PasswordHelper.HashPassword(model.Password),
                CreatedAt = DateTime.Now,
                RoleId = 2
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Json(new { message = "Đăng ký thành công" });
        }
        [HttpPost]
        public JsonResult Login([FromBody] RegisterVM model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin." });
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null || !PasswordHelper.VerifyPassword(model.Password, user.PasswordHash))
            {
                return Json(new { success = false, message = "Tên đăng nhập hoặc mật khẩu không đúng." });
            }

            HttpContext.Session.SetString("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);

            return Json(new
            {
                success = true,
                message = "Đăng nhập thành công!",
                redirectUrl = Url.Action("Index", "Home")
            });
        }

    }
}
