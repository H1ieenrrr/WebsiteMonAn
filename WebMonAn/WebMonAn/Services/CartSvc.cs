using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Interfaces;
using WebMonAn.Models;

namespace WebMonAn.Services
{
    public class CartSvc : ICart
    {
        protected DataContext _context;
     
        public CartSvc(DataContext context)
        {
            _context = context;
        }
        public List<CartModel> GetCartAll()
        {
            List<CartModel> list = new List<CartModel>();

            list = _context.cartModels.OrderByDescending(x => x.NgayDat)
                    .Include(x => x.UserModel)
                    .Include(x => x.cartDetailsModels)
                    .ToList();
            return list;
        }
        public List<CartModel> GetCartbyUser(int userID)
        {
            List<CartModel> list = new List<CartModel>();

            list = _context.cartModels.Where(x => x.UserID == userID).OrderByDescending(x => x.NgayDat)
                    .Include(x => x.UserModel)
                    .Include(x => x.cartDetailsModels)
                    .ToList();
            return list;
        }
        public CartModel GetCart(int id)
        {
            CartModel cart = null;

            cart = _context.cartModels.Where(x => x.CartID == id)
                .Include(x => x.UserModel)
                .Include(x => x.cartDetailsModels)
                .FirstOrDefault();
            return cart;
        }
        public int AddCart(CartModel cart)
        {
            int ret = 0;
            try
            {
                cart.NgayDat = DateTime.Now;
                cart.TrangthaiDonhang = TrangThaiDonHang.Moidat;
                _context.Add(cart);
                _context.SaveChanges();
                ret = cart.CartID;
            }
            catch(Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        public int EditCart(int id, CartModel cart)
        {
            int ret = 0;
            try
            {
                _context.Update(cart);
                _context.SaveChanges();
                ret = cart.CartID;
            }
            catch(Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        public int DeleteCart(int id)
        {
            int ret = 0;
            try
            {
                var cart = _context.cartModels.Where(x => x.CartID == id)
                .Include(x => x.UserModel)
                .Include(x => x.cartDetailsModels)
                .FirstOrDefault();
                _context.Remove(cart);
                _context.SaveChanges();
                ret = cart.CartID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public bool CartExists(int id)
        {
            var cartExists = _context.cartModels.Any(e => e.CartID == id);
            return cartExists;
        }
        public SelectList GetSelectList()
        {
            return new SelectList(_context.userModels, "NguoiDungId", "UserName");
        }
        public SelectList GetSelectList(CartModel cartModel)
        {
            return new SelectList(_context.userModels, "NguoiDungId", "UserName", cartModel.UserID);
        }
    }
}
