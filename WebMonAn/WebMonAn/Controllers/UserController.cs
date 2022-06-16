using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMonAn.Interfaces;
using WebMonAn.Models;

namespace WebMonAn.Controllers
{
    public class UserController : Controller
    {
        private IUser _UserSvc;

        public UserController(IUser UserSvc)
        {
            _UserSvc = UserSvc;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.Role = HttpContext.Session.GetInt32("Role");
            ViewBag.NguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            return View(_UserSvc.GetUserAll());
        }

  
        public IActionResult Details(int id)
        {
            var userModel = _UserSvc.GetUserId(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        public IActionResult DetailsNguoiDung(int id)
        {
            var userModel = _UserSvc.GetUserId(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.Role = HttpContext.Session.GetInt32("Role");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string userName, string passWord)
        {
            
            if (ModelState.IsValid)
            { 

                List<UserModel> user = _UserSvc.Login(userName, passWord);
                
                if (user.Count()>0)
                {
                    HttpContext.Session.SetString("UserName", userName);

                        HttpContext.Session.SetString("FullName", user.FirstOrDefault().FullName.ToString());
                        HttpContext.Session.SetString("DiaChi", user.FirstOrDefault().DiaChi.ToString());
                        HttpContext.Session.SetString("DienThoai", user.FirstOrDefault().DienThoai.ToString());
                        HttpContext.Session.SetString("Email", user.FirstOrDefault().Email.ToString());
                        HttpContext.Session.SetInt32("Role", user.FirstOrDefault().Role);
                    HttpContext.Session.SetInt32("NguoiDungId", user.FirstOrDefault().NguoiDungId);


                    return RedirectToAction("Index", "Food");
                }
                else
                {
                    ViewBag.error = "Users & Mật Khẩu Không Đúng";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index", "Food");
        }

        public IActionResult Create()
        {
            
            ViewData["Role"] = _UserSvc.GetSelectList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel userModel)
        {
            
            if (ModelState.IsValid)
            {
                if (_UserSvc.CheckUser(userModel) == null)
                {
                    _UserSvc.AddUser(userModel);
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ViewBag.error = "Tên Users Đã Tồn Tại";
                    return View();
                }
                
            }
            ViewData["Role"] = _UserSvc.GetSelectList(userModel);
            return View(userModel);
        }

        public IActionResult CreateNhanVien()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.Role = HttpContext.Session.GetInt32("Role");
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNhanVien( UserModel userModel)
        {

            if (ModelState.IsValid)
            {
                if (_UserSvc.CheckUser(userModel) == null)
                {
                    _UserSvc.AddNhanVien(userModel);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.error = "Tên Users Đã Tồn Tại";
                    return View();
                }

            }
            ViewData["Role"] = _UserSvc.GetSelectList(userModel);
            return View(userModel);
        }

        public IActionResult Edit(int id)
        {

            var userModel = _UserSvc.GetUser(id);
            if (userModel == null)
            {
                return NotFound();
            }
            ViewData["Role"] = _UserSvc.GetSelectList(userModel);
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserModel userModel)
        {
            if (id != userModel.NguoiDungId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _UserSvc.EditUser(id, userModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.NguoiDungId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Role"] = _UserSvc.GetSelectList(userModel);
            return View(userModel);
        }

   
        public IActionResult Delete(int id)
        {

            var userModel = _UserSvc.GetUserId(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
                var userModel = _UserSvc.DeleteUser(id);
                return RedirectToAction(nameof(Index));

        }

        private bool UserModelExists(int id)
        {
            return _UserSvc.UserExists(id);
        }


    }
}
