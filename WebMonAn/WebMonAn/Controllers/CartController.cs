using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMonAn.Helpers;
using WebMonAn.Interfaces;
using WebMonAn.Models;

namespace WebMonAn.Controllers
{
    public class CartController : Controller
    {
        private ICart _cartSvc;
        private IFood _foodSvc;
        private ICartDetails _cartDetailsSvc;
        public CartController(ICart cartsSvc, IFood foodSvc, ICartDetails cartDetails)
        {
            _cartSvc = cartsSvc;
            _foodSvc = foodSvc;
            _cartDetailsSvc = cartDetails;
        }


        public IActionResult Buy(int id)
        {
            if(SessionHelper.GetObjectFromJson<List<CartDetailsModel>>(HttpContext.Session, "cart") == null)
            {
                List<CartDetailsModel> cart = new List<CartDetailsModel>();
                cart.Add(new CartDetailsModel { foodModel = _foodSvc.GetFood(id), Soluong = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartDetailsModel> cart = SessionHelper.GetObjectFromJson<List<CartDetailsModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if(index != -1)
                {
                    cart[index].Soluong++;
                }
                else
                {
                    cart.Add(new CartDetailsModel { foodModel = _foodSvc.GetFood(id), Soluong = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index","Food");
        }

        private int isExist(int id)
        {
            List<CartDetailsModel> cart = SessionHelper.GetObjectFromJson<List<CartDetailsModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].foodModel.MonAnId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult Remove(int id)
        {
            List<CartDetailsModel> cart = SessionHelper.GetObjectFromJson<List<CartDetailsModel>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Create");
        }

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.DienThoai = HttpContext.Session.GetString("DienThoai");
            ViewBag.DiaChi = HttpContext.Session.GetString("DiaChi");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Role = HttpContext.Session.GetInt32("Role");
            //var dataContext = _context.cartModels.Include(c => c.UserModel);
            return View(_cartSvc.GetCartAll());
        }

       
        public IActionResult Details(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");

            var cartModel = _cartSvc.GetCart(id);
                //await _context.cartModels.Include(c => c.UserModel)
                //.FirstOrDefaultAsync(m => m.CartID == id);
            if (cartModel == null)
            {
                return NotFound();
            }

            return View(cartModel);
        }

        public IActionResult Create()
        {
            double a = 30000;

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            ViewBag.DienThoai = HttpContext.Session.GetString("DienThoai");
            ViewBag.DiaChi = HttpContext.Session.GetString("DiaChi");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Role = HttpContext.Session.GetInt32("Role");

            var cart = SessionHelper.GetObjectFromJson<List<CartDetailsModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.foodModel.GiaMonAn * item.Soluong);
            ViewBag.total1 = ViewBag.total + a;
            ViewData["NguoiDungId"] = HttpContext.Session.GetInt32("NguoiDungId");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CartModel cartModel)
        {
            double a = 30000;
            var cart = SessionHelper.GetObjectFromJson<List<CartDetailsModel>>(HttpContext.Session, "cart");
            ViewBag.total1 = cart.Sum(item => (item.foodModel.GiaMonAn * item.Soluong) + a);
            if (ModelState.IsValid)
            {
                _cartSvc.AddCart(cartModel);


                foreach (var item in cart)
                {
                    CartDetailsModel cartDetailsModel = new CartDetailsModel();
                    cartDetailsModel.CartID = cartModel.CartID;
                    cartDetailsModel.MonAnId = item.foodModel.MonAnId;
                    cartDetailsModel.TenMonAn = item.foodModel.TenMonAn;
                    cartDetailsModel.Hinh = item.foodModel.Hinh;
                    cartDetailsModel.GiaMonAn = item.foodModel.GiaMonAn;
                    cartDetailsModel.Soluong = item.Soluong;
                    cartDetailsModel.ThanhTien = ViewBag.total1;
                    _cartDetailsSvc.AddCartDetails(cartDetailsModel);
                }
                HttpContext.Session.Remove("cart");
                return RedirectToAction(nameof(Display));
            }
            ViewData["NguoiDungId"] = HttpContext.Session.GetString("UserName");
            return View(cartModel);
        }
        public IActionResult Display()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            return View();
        }
        // GET: Cart/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            var cartModel = _cartSvc.GetCart(id);
            if (cartModel == null)
            {
                return NotFound();
            }
            //ViewData["UserID"] = new SelectList(_context.userModels, "NguoiDungId", "ConfirmPassword", cartModel.UserID);
            return View(cartModel);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CartID,UserID,NgayDat,Tongtien,TrangthaiDonhang,GhiChu")] CartModel cartModel)
        {
            if (id != cartModel.CartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //cartModel.UserID = null;
                    _cartSvc.EditCart(id, cartModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartModelExists(cartModel.CartID))
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
            //ViewData["UserID"] = new SelectList(_context.userModels, "NguoiDungId", "ConfirmPassword", cartModel.UserID);
            return View(cartModel);
        }

        // GET: Cart/Delete/5
        public IActionResult Delete(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");

            var cartModel = _cartSvc.GetCart(id);
            if (cartModel == null)
            {
                return NotFound();
            }

            return View(cartModel);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cartModel = _cartSvc.DeleteCart(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CartModelExists(int id)
        {
            return _cartSvc.CartExists(id);
        }
    }
}
