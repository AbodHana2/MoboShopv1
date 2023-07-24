using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoboShopv1.Models;
using MoboShopv1.Models.Interfaces;
using System.IO;

namespace MoboShopv1.Controllers
{
    [Authorize(Roles ="Admin,Saler")]
    public class MangerController : Controller
    {
        private readonly IUnitofWork _unit;

        public MangerController(IUnitofWork unit)
        {
            _unit = unit;
        }

        [Authorize(Roles = "Admin,Saler")]
        public ActionResult List()
        {
            var products = _unit.product.GetAll();
            return View(products);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Saler")]
        public ActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Saler")]
        public ActionResult Create([Bind("Title,ShortDescription,LongDescription,ImageURL1,ImageURL2,Price,Category,ImageFile1,ImageFile2")]Product product)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded image files
                if (product.ImageFile1 != null && product.ImageFile1.Length > 0)
                {
                    string Filename = product.ImageFile1.FileName;
                   // string ex = Path.GetExtension(product.ImageFile1.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploaded", Filename);

                    var stream = new FileStream(path , FileMode.Create);
                    product.ImageFile1.CopyTo(stream);

                    string url = "/Uploaded/" + Filename;
                    product.ImageURL1 = url;
                }

                if (product.ImageFile2 != null && product.ImageFile2.Length > 0)
                {
                    string Filename =product.ImageFile2.FileName;
                  //  string ex = Path.GetExtension(product.ImageFile2.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploaded", Filename);

                    var stream = new FileStream(path, FileMode.Create);
                    product.ImageFile2.CopyTo(stream);

                    string url = "/Uploaded/" + Filename ;
                    product.ImageURL2 = url;
                }

                var result = _unit.product.Add(product);
                if(result > 0)
                {
                    TempData["Alertmessage"] = "The Product has been Created Successfully";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["ErrorMessage"] = " Somthing Went wrong !!!!";
                    return View(product);
                }
            }
            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Saler")]
        public ActionResult Edit(int Id)
        {
            if(Id == 0)
            {
                return RedirectToAction("Index");
            }
            var prod = _unit.product.GetOne(Id);
            if (prod == null)
            {
                return RedirectToAction("List");
            }
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Saler")]
        public ActionResult Edit([Bind("ID,Title,ShortDescription,LongDescription,ImageURL1,ImageURL2,Price,Category,ImageFile1,ImageFile2")]Product product)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded image files
                if (product.ImageFile1 != null && product.ImageFile1.Length > 0)
                {
                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(product.ImageURL1))
                    {
                        string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageURL1.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save the new image
                    string filename = product.ImageFile1.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploaded", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        product.ImageFile1.CopyTo(stream);
                    }

                    string url = "/Uploaded/" + filename;
                    product.ImageURL1 = url;
                }
                // Save the uploaded image files
                if (product.ImageFile2 != null && product.ImageFile2.Length > 0)
                {
                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(product.ImageURL2))
                    {
                        string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageURL2.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save the new image
                    string filename = product.ImageFile2.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploaded", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        product.ImageFile2.CopyTo(stream);
                    }

                    string url = "/Uploaded/" + filename;
                    product.ImageURL2 = url;
                }
                var result = _unit.product.Edit(product);
                if (result)
                {
                    TempData["Alertmessage"] = "The Product has been Updated Successfully";
                    return RedirectToAction("List");
                    
                }
                else
                {
                    TempData["ErrorMessage"] = " Somthing Went wrong !!!!";
                    return View(product);
                }
            }
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Saler")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return RedirectToAction("List");
            if(ModelState.IsValid)
            {
                _unit.product.Remove(id);
            }
            return RedirectToAction("List");
        }

    }
}
