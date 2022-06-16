using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Interfaces;
using WebMonAn.Models;

namespace WebMonAn.Services
{
    public class FoodSvc : IFood
    {
        protected DataContext _context;

        public FoodSvc (DataContext context)
        {
            _context = context;
        }
        
        public List<FoodModel> GetFoodAll()
        {
            List<FoodModel> list = new List<FoodModel>();
            list = _context.foodModels.Include(f => f.categoryModel).ToList();
            return list;
        }

        public FoodModel GetFoodId(int id)
        {
            FoodModel food = null;
            food = _context.foodModels
                .Include(f => f.categoryModel)
                .FirstOrDefault(m => m.MonAnId == id);
            return food;
        }

        public FoodModel GetFood(int id)
        {
            FoodModel food = null;
            food = _context.foodModels.Find(id);
            return food;
        }
        public int AddFood(FoodModel foodModel)
        {
            int ret = 0;
            try
            {
                _context.Add(foodModel);
                _context.SaveChanges();
                ret = foodModel.MonAnId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public int EditFood(int id, FoodModel foodModel)
        {
            int ret = 0;
            try
            {
                _context.Update(foodModel);
                _context.SaveChanges();
                ret = foodModel.MonAnId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int DeleteFood(int id)
        {
            int ret = 0;
            try
            {
                var food = _context.foodModels.Find(id);
                _context.Remove(food);
                _context.SaveChanges();
                ret = food.MonAnId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public bool FoodExists(int id)
        {
            return _context.foodModels.Any(e => e.MonAnId == id);
        }

        public SelectList GetSelectList()
        {
          return  new SelectList(_context.categoryModels, "CategoryId", "CategoryName");
        }

        public SelectList GetSelectList(FoodModel foodModel)
        {
           return new SelectList(_context.categoryModels, "CategoryId", "CategoryName", foodModel.Category);
        }
    }
}
