using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Models;

namespace WebMonAn.Interfaces
{
    public interface IFood
    {
        List<FoodModel> GetFoodAll();

        FoodModel GetFood(int id);

        FoodModel GetFoodId(int id);

        int AddFood(FoodModel foodModel);

        int EditFood(int id, FoodModel foodModel);

        int DeleteFood(int id);

        bool FoodExists(int id);

        SelectList GetSelectList();

        SelectList GetSelectList(FoodModel foodModel);


    }
}
