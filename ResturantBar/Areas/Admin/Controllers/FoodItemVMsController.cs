using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResturantBar.Areas.Admin.Models.Vm;
using ResturantBar.Models;
using ResturantBar.Areas.Admin.Models;
using System.IO;

namespace ResturantBar.Areas.Admin.Controllers
{
    public class FoodItemVMsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/FoodItemVMs
        public ActionResult Index()
        {
            return View(db.FoodItems.ToList());
        }

        // GET: Admin/FoodItemVMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // GET: Admin/FoodItemVMs/Create
        public ActionResult Create()
        {
            //Init the model
            FoodItemVM model = new FoodItemVM();

                model.FoodTypes = new SelectList(db.FoodTypes.ToList(), "Id", "Name");
    
            //Return model with view
            return View(model);



        }

        // POST: Admin/FoodItemVMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodItemVM model , HttpPostedFileBase file)
        {


            //Make sure product name unique

                if (db.FoodItems.Any(x => x.FoodName == model.FoodName))
                {
                model.FoodTypes = new SelectList(db.FoodTypes.ToList(), "Id", "Name");
                ModelState.AddModelError("", "That product name is already taken!");
                    return View(model);
                }


            var originalDirectory = new DirectoryInfo(string.Format("{0}Content\\Images", Server.MapPath(@"\\")));

            var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
            if (!Directory.Exists(pathString1))
            {
                Directory.CreateDirectory(pathString1);
            }

            //Check if a file was uploaded
            if (file != null && file.ContentLength > 0)
            {
                //Get file extension
                string ext = file.ContentType.ToLower();
                //verify extension
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/png" &&
                    ext != "image/pjpeg")
                {
                    ModelState.AddModelError("", "That Image was not uploaded, wrong format.");
                    return View(model);

                }
                //init image name
                string imageName = file.FileName;

                //set original and thumb image paths
                var path = string.Format("{0}\\{1}", pathString1, imageName);
                //save original image
                file.SaveAs(path);
                //create and save thumb            
            }







            int id;
            //init and save productDTO
            int dd = Convert.ToInt32(model.FoodTypeName);
            FoodType type = db.FoodTypes.FirstOrDefault(x => x.Id ==dd );
            FoodItem food = new FoodItem();
                food.FoodName = model.FoodName;
             
                food.FoodTypeName = type.Name;
                food.Price = model.Price;
            food.Image = file.FileName;

                db.FoodItems.Add(food);
                db.SaveChanges();

                //get the id
                id = food.Id;


            //save tempdata message
            TempData["SM"] = "You have added a product";



            return RedirectToAction("Create");


        }

        // GET: Admin/FoodItemVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // POST: Admin/FoodItemVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodItem);
        }

        // GET: Admin/FoodItemVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // POST: Admin/FoodItemVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItem foodItem = db.FoodItems.Find(id);
            db.FoodItems.Remove(foodItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
