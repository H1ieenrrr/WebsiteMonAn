using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Models;

namespace WebMonAn.Interfaces
{
    public interface ICart
    {
        List<CartModel> GetCartAll();

        List<CartModel> GetCartbyUser(int userID);

        CartModel GetCart(int id);

        int AddCart(CartModel cart);

        int EditCart(int id, CartModel cart);

        int DeleteCart(int id);

        bool CartExists(int id);

        SelectList GetSelectList();
        SelectList GetSelectList(CartModel cartModel);
    }
}
