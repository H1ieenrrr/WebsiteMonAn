using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMonAn.Models;

namespace WebMonAn.Interfaces
{
    public interface IUser
    {
        List<UserModel> GetUserAll();
        UserModel GetUserId(int id);
        UserModel GetUser(int id);
        int AddUser(UserModel userModel);
        int AddNhanVien(UserModel userModel);
        int EditUser(int id, UserModel userModel);
        int DeleteUser(int id);
        bool UserExists(int id);
        SelectList GetSelectList();
        SelectList GetSelectList(UserModel userModel);
        UserModel CheckUser(UserModel user);
        List<UserModel> Login(string userName, string passWord);
        RoleModel CheckAdmin();

    }
}
