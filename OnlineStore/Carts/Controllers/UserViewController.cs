using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class UserViewController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //會員資料
        public ActionResult UserView(string UserName)
        {
            var getName = HttpContext.User.Identity.GetUserName().ToString();
            using (Models.UserEntities db = new Models.UserEntities())
            {   //抓取指定AspNetUsers中的資料，並且放入Models.ManageUser模型中
                var result = (from s in db.AspNetUsers
                              where s.UserName == getName //抓取暱稱
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).ToString();
                return View(result);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(Models.ManageUser postback)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == postback.Id
                              select s).FirstOrDefault();
                if (result != default(Models.AspNetUser))
                {
                    result.UserName = postback.UserName;
                    result.Email = postback.Email;
                    db.SaveChanges();
                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format("使用者[{0}]成功編輯", postback.UserName);
                    return RedirectToAction("Index");
                }
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", postback.UserName);
            return RedirectToAction("Index");
        }
    }
}
