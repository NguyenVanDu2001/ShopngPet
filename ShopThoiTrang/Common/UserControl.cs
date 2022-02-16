using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopThoiTrang.Common
{
    public class UserLoginControl
    {
        public static Userlogin CurrentUser
        {
            get
            {
                return HttpContext.Current.Session[CommonConstants.USER_SESSION] as Userlogin;
            }
        }
        public static bool HasPermission(string requiredPermission)
        {
            if (CurrentUser != null)
            {
                if (!CurrentUser.HasPermission(requiredPermission))
                {
                    if (requiredPermission.IndexOf(":") > 0)
                    {
                        requiredPermission = requiredPermission.Substring(0, requiredPermission.IndexOf(":")) + ":*";
                        if (!CurrentUser.HasPermission(requiredPermission))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}