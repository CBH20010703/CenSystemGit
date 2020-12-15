using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CenBolgsSystem.Models;
namespace CenBolgsSystem.App_Start.Filters
{
    public class LogFilterAttribute: ActionFilterAttribute,IActionFilter
    {
        /// <summary>
        /// 操作员Id
        /// </summary>
       
        public LogFilterAttribute(string operate,int operateStatus)
        {  // 操作名称
            this.log_Content = operate;
            this.log_OperStatus = operateStatus;
            // 操作时间
            this.log_CreatDataTime = DateTime.Now;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {  //获取 控制器名 与 动作名 

            log_OperAction = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName+filterContext.ActionDescriptor.ActionName;
            log_OperatelogAdmin = (int)filterContext.HttpContext.Session["AdminId"];
            //获取Controller 名称
            db_CenSystemEntities db = new db_CenSystemEntities();
            db.Operatelog.Add(new Operatelog
            {
                log_CreatDataTime = this.log_CreatDataTime,
                 log_Content=this.log_Content,
                 log_OperAction=this.log_OperAction,
                 log_OperatelogAdmin=this.log_OperatelogAdmin,
                 log_OperStatus=this.log_OperStatus
            });
            db.SaveChanges();
          //  var Request = filterContext.RequestContext.HttpContext.Request.Form;//获取控制器传入的参数
           /// var  str1= filterContext.Result;
            
            //var Time = filterContext.HttpContext.Timestamp;
            //var Code = filterContext.RequestContext;

            //执行完action后跳转后执行
        }
        //操作管理员Id
        public int log_OperatelogAdmin { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime log_CreatDataTime { get; set; }
        /// <summary>
        /// 具体操作
        /// </summary>
        public string log_Content { get; set; }
        //操作危险系数
        public int log_OperStatus { get; set; }
         
        //操作控制器和动作 地址  
        public string log_OperAction { get; set; }
    }
}