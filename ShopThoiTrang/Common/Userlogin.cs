using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ShopThoiTrang.Common
{
    [Serializable]
    public class Userlogin:IPrincipal
    {
        public bool HasPermission(string permission)
        {
            if (Permissions.Any(r => permission.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public long UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
        public string AccessName { set; get; }
        public string[] Permissions { get; set; }
        public string[] Roles { get; set; }

        public IIdentity Identity => throw new NotImplementedException();
    }
}