using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CenBolgsSystem.App_Start.Filters
{
        /// <summary>
        ///  管理员权限管理
        /// </summary>
    public class SuperSUProFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}