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
    public class UserSvc : IUser
    {
        protected DataContext _context;
        protected IMahoa _mahoaHelper;
        public UserSvc(DataContext context, IMahoa mahoaHelper)
        {
            _context = context;
            _mahoaHelper = mahoaHelper;
        }

        public List<UserModel> GetUserAll()
        {
            List<UserModel> list = new List<UserModel>();
            list = _context.userModels.Include(u => u.roleModel).ToList();
            return list;
        }

        public UserModel GetUserId(int id)
        {
            UserModel user = null;
            user = _context.userModels
                .Include(u => u.roleModel)
                .FirstOrDefault(m => m.NguoiDungId == id);
            return user;
        }

        public UserModel GetUser(int id)
        {
            UserModel user = null;
            user = _context.userModels.Find(id);
            return user;
        }


        public int AddUser(UserModel userModel)
        {
            int ret = 0;
            try
            {
                int role = 1;
                userModel.Role = role;
                bool IsDel = true;
                bool status = true;
                userModel.Status = status;
                userModel.IsDelete = IsDel;

                userModel.Password = _mahoaHelper.Mahoa(userModel.Password);
                userModel.ConfirmPassword = _mahoaHelper.Mahoa(userModel.ConfirmPassword);
                _context.Add(userModel);
                _context.SaveChanges();
                ret = userModel.NguoiDungId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int AddNhanVien(UserModel userModel)
        {
            int ret = 0;
            try
            {
                int role = 2;
                userModel.Role = role;
                bool IsDel = true;
                bool status = true;
                userModel.Status = status;
                userModel.IsDelete = IsDel;

                userModel.Password = _mahoaHelper.Mahoa(userModel.Password);
                userModel.ConfirmPassword = _mahoaHelper.Mahoa(userModel.ConfirmPassword);
                _context.Add(userModel);
                _context.SaveChanges();
                ret = userModel.NguoiDungId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int EditUser(int id, UserModel userModel)
        {
            int ret = 0;
            try
            {
                UserModel _user = null;
                _user = _context.userModels.Find(id);

                _user.UserName = userModel.UserName;
                _user.FullName = userModel.FullName;
                _user.Email = userModel.Email;
                _user.GioiTinh = userModel.GioiTinh;
                _user.NgaySinh = userModel.NgaySinh;
                _user.DiaChi = userModel.DiaChi;
                _user.DienThoai = userModel.DienThoai;
                _user.Role = userModel.Role;
                _user.Status = userModel.Status;
                if (_user.Password != null)
                {
                    userModel.Password = _mahoaHelper.Mahoa(userModel.Password);
                    
                    _user.Password = userModel.Password;
                    _user.ConfirmPassword = userModel.Password;
                }

                _context.Update(_user);
                _context.SaveChanges();
                ret = userModel.NguoiDungId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int DeleteUser(int id)
        {
            int ret = 0;
            try
            {
                var user = _context.userModels.Find(id);
                _context.Remove(user);
                _context.SaveChanges();
                ret = user.NguoiDungId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public bool UserExists(int id)
        {
            return _context.userModels.Any(e => e.NguoiDungId == id);
        }

        public SelectList GetSelectList()
        {
            return new SelectList(_context.roleModels, "RoleId", "RoleName");
        }

        public SelectList GetSelectList(UserModel userModel)
        {
            return new SelectList(_context.roleModels, "RoleId", "RoleName", userModel.Role);
        }

        public UserModel CheckUser(UserModel user)
        {
            var checkUsers = _context.userModels.FirstOrDefault(s => s.UserName == user.UserName);
            
            return checkUsers;
        }

        public RoleModel CheckAdmin()
        {
            int admin = 3;
            var roleModel = _context.roleModels.FirstOrDefault(s => s.RoleId == admin);

            return roleModel;
        }

        public List<UserModel> Login(string userName, string passWord)
        {
            var mahoaPassWord = _mahoaHelper.Mahoa(passWord);
            var user = _context.userModels.Where(s => s.UserName.Equals(userName) && s.Password.Equals(mahoaPassWord)).ToList();
            return user;
        }


    }
}
