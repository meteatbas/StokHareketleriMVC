using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;

namespace Asp.NETMVCCRUD.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModelll db = new DBModelll())
            {
                List<Product> empList = db.Products.ToList<Product>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Product());
            else
            {
                using (DBModelll db = new DBModelll())
                {
                    return View(db.Products.Where(x => x.ProductID==id).FirstOrDefault<Product>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Product emp)
        {
            using (DBModelll db = new DBModelll())
            {
                if (emp.ProductID == 0)
                {
                    db.Products.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModelll db = new DBModelll())
            {
                Product emp = db.Products.Where(x => x.ProductID == id).FirstOrDefault<Product>();
                db.Products.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Ürün Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            if (id == 0)
                return View(new Product());
            else
            {
                using (DBModelll db = new DBModelll())
                {
                    return View(db.Products.Where(x => x.ProductID == id).FirstOrDefault<Product>());
                }
            }
        }


        [HttpPost]
        public ActionResult Add(Product emp)
        {
            using (DBModelll db = new DBModelll())
            {
                if (emp.ProductID == 0)
                {
                    db.Products.Add(emp);
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Ürün Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }


               
                
            }
        }
    }
}