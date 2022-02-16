using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopThoiTrang.Common
{
    public class UserControlService
    {
        public static ShopThoiTrangDbContext _context = new ShopThoiTrangDbContext();
        public static string[] GetPermission(int userId)
        {

            List<string> listPermissions = new List<string>();
            using (ShopThoiTrangDbContext _context = new ShopThoiTrangDbContext())
            {
                try
                {
                    listPermissions = (from userPermission in _context.UserPermissions
                                       join userRole in _context.UserRoles on userPermission.RoleId equals userRole.RoleId
                                       where userPermission.UserId == userId && userRole.UserId == userId
                                       select userPermission.Permission).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                return listPermissions.ToArray();
            }
        }

        public static string[] GetRole(int userId)
        {
            List<string> listRole = new List<string>();
            using (ShopThoiTrangDbContext _context = new ShopThoiTrangDbContext())
            {
                try
                {
                    listRole = (from userRole in _context.UserRoles
                                join role in _context.Roles on userRole.RoleId equals role.parentId
                                where userRole.UserId == userId
                                select role.accessName).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                return listRole.ToArray();
            }
        }
    }
}