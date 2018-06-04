using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Carts.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using( Models.CartsEntities db = new Models.CartsEntities() )
            {
                var result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }
        //衣服類別為1
        public ActionResult ClothesCategory()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products where s.CategoryId == 1 select s).ToList();
                return View(result);
            }
        }

        //長T-shirt類別為11
        public ActionResult LongClothesCategory()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products where s.CategoryId == 11 select s).ToList();
                return View(result);
            }
        }

        //短T-shirt類別為12
        public ActionResult ShortClothesCategory()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products where s.CategoryId == 12 select s).ToList();
                return View(result);
            }
        }

        //外套類別為13
        public ActionResult CoatClothesCategory()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products where s.CategoryId == 13 select s).ToList();
                return View(result);
            }
        }

        //褲子類別為2
        public ActionResult PantsCategory()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products where s.CategoryId == 2 select s).ToList();
                return View(result);
            }
        }
        //襪子類別為3
        public ActionResult SockCategory()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products where s.CategoryId == 3 select s).ToList();
                return View(result);
            }
        }
        //配件類別為4
        public ActionResult AccessoriesCategory()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products where s.CategoryId == 4 select s).ToList();
                return View(result);
            }
        }


        public ActionResult Details(int id)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products 
                              where s.Id == id
                              select s).FirstOrDefault();

                if (result == default(Models.Product))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }

        [HttpPost]  //限定使用POST
        [Authorize] //登入會員才可留言
        public ActionResult AddComment(int id, string Content)
        {
            //取得目前登入使用者Id
            var userId = HttpContext.User.Identity.GetUserId();

            var currentDateTime = DateTime.Now;

            var comment = new Models.ProductCommet() { 
                ProductId = id,
                Content = Content,
                UserId = userId,
                CreateDate = currentDateTime
            };

            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                db.ProductCommets.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
        }





        public ActionResult Index2()
        {
            return Content(
                "<html><body><h1>這是一段訊息</h1></body></html>"
                );
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}