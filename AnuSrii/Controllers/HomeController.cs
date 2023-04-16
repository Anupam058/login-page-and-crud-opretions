using AnuSrii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace AnuSrii.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(ForLogin obj)
        {
            if (ModelState.IsValid == true)
            {
                ContextDb db = new ContextDb();
                bool cheak = db.LoginEmployee(obj);
                if (cheak == true)
                {
                    Session["insert"] = "welcome to my page";
                    ModelState.Clear();
                    return RedirectToAction("Welcome");
                }
                else
                {
                    ViewData["mmm"] = "!!!Username Or Password Is Wrong";
                }
            }

            return View();
        }
        public ActionResult Welcome()
        {
            if (Session["insert"]!= null) 
            {
            return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Details() 
        {
            if (Session["insert"] != null)
            {
                ContextDb db = new ContextDb();
                List<EmployeeDetails> obj = db.GetEmployees();
                return View(obj);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }           

          
        

        public ActionResult Create()
        {
            if (Session["insert"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public ActionResult Create(EmployeeDetails emp)
        {
            try
            {

                if (ModelState.IsValid == true)
                {
                    ContextDb db = new ContextDb();
                    bool cheak = db.AddEmployee(emp);
                    if (cheak == true)
                    {
                        TempData["insert"] = "Data insert Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Details");
                    }

                }
                return View();
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Edit(int Id)
        {
            if (Session["insert"] != null)
            {
                ContextDb context = new ContextDb();
                var row = context.GetEmployees().Find(model => model.Id == Id);
                return View(row);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int Id, EmployeeDetails emp)
        {
                if (ModelState.IsValid == true)
                {
                    ContextDb ctx = new ContextDb();
                    bool cheak = ctx.UpdateEmployee(emp);
                    if (cheak == true)
                    {
                        TempData["update"] = "Data updated Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Details");
                    }
                }
            
           return View();
        }



        public ActionResult Delete(int Id)
        {
            if (Session["insert"] != null) 
            {
            ContextDb context = new ContextDb();
            var row = context.GetEmployees().Find(model => model.Id == Id);
            return View(row);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int Id, EmployeeDetails emp)
        {
            ContextDb context = new ContextDb();
            bool cheak = context.DeleteEmployee(Id);
            if (cheak == true)
            {
                TempData["delete"] = "Data deleted Successfully";
                ModelState.Clear();
                return RedirectToAction("Details");
            }

            return View();
        }

        public ActionResult Detail(int Id)
        {
            if (Session["insert"] != null)
            {
                ContextDb context = new ContextDb();
                var row = context.GetEmployees().Find(model => model.Id == Id);
                return View(row);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    







    }
}