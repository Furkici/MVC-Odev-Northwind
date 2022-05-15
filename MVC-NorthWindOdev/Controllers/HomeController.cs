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

        public static List<Customer> customers = new List<Customer>();
        public static List<Order> orders = new List<Order>();

        // GET: Home
        public ActionResult Index()
        {
            //Listeleme işlemleri
            customers = db.Customers.OrderByDescending(x => x.CustomerID).ToList();
            orders = db.Orders.OrderBy(x => x.OrderID).ToList();

            List<Employee> employees = db.Employees.OrderBy(x => x.EmployeeID).ToList();
            List<Product> products = db.Products.OrderBy(x=> x.ProductID).ToList();

            //Sayma İşlemleri
            TempData["Toplamürün"] = products.Count();
            TempData["Çalışanlar"]=employees.Count();
            TempData["Siparişadet"]=orders.Count();
            TempData["Toplammüşteri"]=customers.Count();

            //Listeleme tempdata
            //TempData["Orders"] = orders.ToList();
            TempData["Customers"] = customers.Take(20).ToList();
            TempData["Sonsiparişler"]=orders.OrderByDescending(x => x.OrderDate).Take(20).ToList();
            TempData.Keep();
            return View();
        }

        //Müşteri Detayı
        public ActionResult CustomerDetails(string id)
        {
            var customerdetay = customers.Find(x => x.CustomerID == id);

            return View(customerdetay);
            
        }

        //Sipariş Detayı
        public ActionResult OrderDetails(int id)
        {
            var orderdetay = orders.Find(x => x.OrderID == id);  
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
            
            customers.Add(customer);

            return RedirectToAction("Index");
        }
    }
}