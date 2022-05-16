using MVC_NorthWindOdev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_NorthWindOdev.Controllers
{
    public class HomeController : Controller
    {
        NORTHWNDEntities db = new NORTHWNDEntities();

        

        // GET: Home
        public ActionResult Index()
        {
            
             
            //Listeleme işlemleri
            List<Customer> customers = db.Customers.OrderByDescending(x => x.CustomerID).ToList();
            List<Order> orders = db.Orders.OrderBy(x => x.OrderID).ToList();

            List<Employee> employees = db.Employees.OrderBy(x => x.EmployeeID).ToList();
            List<Product> products = db.Products.OrderBy(x=> x.ProductID).ToList();

            //Sayma İşlemleri
            TempData["Toplamürün"] = products.Count();
            TempData["Çalışanlar"]=employees.Count();
            TempData["Siparişadet"]=orders.Count();
            TempData["Toplammüşteri"]=customers.Count();

            //Listeleme tempdata
            TempData["Customers"] = customers.Take(20).ToList();
            TempData["Sonsiparişler"]=orders.OrderByDescending(x => x.OrderDate).Take(20).ToList();
            TempData.Keep();
            return View();
        }

        //Müşteri Detayı
        public ActionResult CustomerDetails(string id)
        {
            var customerdetay = db.Customers.ToList().Find(x => x.CustomerID == id);

            return View(customerdetay);
            
        }

        //Sipariş Detayı
        public ActionResult OrderDetails(int id)
        {
            var orderdetay = db.Orders.ToList().Find(x => x.OrderID == id); 
            return View(orderdetay);
        }

        //Müşteri Ekleme
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}