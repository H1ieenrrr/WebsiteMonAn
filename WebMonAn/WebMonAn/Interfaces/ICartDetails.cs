using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Models;

namespace WebMonAn.Interfaces
{
    public interface ICartDetails
    {
        int AddCartDetails(CartDetailsModel cartDetailsModel);
        List<CartDetailsModel> GetCartDetailsAll();
    }
}
