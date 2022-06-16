using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMonAn.Helpers;
using WebMonAn.Interfaces;
using WebMonAn.Models;

namespace WebMonAn.Controllers
{
    public class FoodController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private IFood _FoodSvc;
        private IUpload _uploadHelper;

        public FoodController(IWebHostEnvironment webHostEnvironment, IFood FoodSvc,
                IUpload uploadHelper)
        {
            _webHostEnviroment = webHostEnvironment;
            _FoodSvc = FoodSvc;
            _uploadHelper = uploadHelper;
        }

        // GET: Food
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartDetailsModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.Role = HttpContext.Session.GetInt32("Role");
            ViewBag.NguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            return View(_FoodSvc.GetFoodAll());
        }

        // GET: Food/Details/5
        public IActionResult Details(int id)
        {
            var foodModel = _FoodSvc.GetFoodId(id);
            if (foodModel == null)
            {
                return NotFound();
            }

            return View(foodModel);
        }

        public IActionResult Create()
        {
            ViewData["Category"] = _FoodSvc.GetSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FoodModel foodModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (foodModel.ImageFile != null)           
                    { 
                        _uploadHelper.UploadFile(foodModel.ImageFile);
                        foodModel.Hinh = foodModel.ImageFile.FileName;
                       
                    }
                    _FoodSvc.AddFood(foodModel);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ViewData["Category"] = _FoodSvc.GetSelectList(foodModel);
            return View(foodModel);
        }

        public IActionResult Edit(int id)
        {

            var foodModel = _FoodSvc.GetFood(id);
            if (foodModel == null)
            {
                return NotFound();
            }
            ViewData["Category"] = _FoodSvc.GetSelectList(foodModel);
            return View(foodModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,FoodModel foodModel)
        {
            if (id != foodModel.MonAnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (foodModel.ImageFile != null)
                    {
                        _uploadHelper.UploadFile(foodModel.ImageFile);
                        foodModel.Hinh = foodModel.ImageFile.FileName;
                    }
                    _FoodSvc.EditFood(id, foodModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodModelExists(foodModel.MonAnId))
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
            ViewData["Category"] = _FoodSvc.GetSelectList(foodModel);
            return View(foodModel);
        }

        public IActionResult Delete(int id)
        {
            var foodModel = _FoodSvc.GetFoodId(id);
            if (foodModel == null)
            {
                return NotFound();
            }

            return View(foodModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var foodModel = _FoodSvc.DeleteFood(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FoodModelExists(int id)
        {
            return _FoodSvc.FoodExists(id);
        }
    }
}
