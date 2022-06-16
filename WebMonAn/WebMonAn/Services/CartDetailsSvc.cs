using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Interfaces;
using WebMonAn.Models;

namespace WebMonAn.Services
{
    public class CartDetailsSvc : ICartDetails
    {
        protected DataContext _context;

        public CartDetailsSvc(DataContext context)
        {
            _context = context;
        }
        public List<CartDetailsModel> GetCartDetailsAll()
        {
            List<CartDetailsModel> list = new List<CartDetailsModel>();
            list = _context.cartDetailsModels.Include(c => c.cartModel).Include(c => c.foodModel).ToList();
            return list;
        }
        public int AddCartDetails(CartDetailsModel cartDetailsModel)
        {
            int ret = 0;
            try
            {
                _context.Add(cartDetailsModel);
                _context.SaveChanges();
                ret = cartDetailsModel.CartDetailsID;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
