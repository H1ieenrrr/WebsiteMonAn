using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMonAn.Interfaces;
using WebMonAn.Models;

namespace WebMonAn.Controllers
{
    public class CartDetailsController : Controller
    {
        private readonly ICartDetails _cartDetails;

        public CartDetailsController(ICartDetails cartDetails)
        {
            _cartDetails = cartDetails;
        }

        // GET: CartDetails
        public IActionResult _CartDetails()
        {
            
            return View(_cartDetails.GetCartDetailsAll());
        }

    }
}
