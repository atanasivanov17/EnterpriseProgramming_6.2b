using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemsService _itemsService;
        private readonly CategoriesService _categoriesService;
        private IWebHostEnvironment _hostEnvironment;
        public ItemsController(ItemsService itemsService, CategoriesService categoriesService, IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;
            _itemsService = itemsService;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateItemViewModel newModel = new CreateItemViewModel();
            newModel.Categories = _categoriesService.GetCategories();
 
            return View(newModel);
        }

        [HttpPost]
        public IActionResult Create(CreateItemViewModel data, IFormFile file)
        {
            try
            {

                if(file != null)
                {
                    string uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    string absolutePath = Path.Combine(_hostEnvironment.WebRootPath, "images", uniqueFilename);

                    using (var stream = System.IO.File.Create(absolutePath))
                    {
                        file.CopyTo(stream);
                    }

                    data.ImagePath = "/images/" + uniqueFilename;
                    
                }


                _itemsService.AddNewItem(data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);

                //ViewBag.Message = "Item added successfully";
                TempData["success"] = "Item added successfully";
            }
            catch (Exception ex)
            {
                //log the exception
                //ViewBag.Error = "There was a problem adding a new item. Make sure all the fields are correctly filled";
                TempData["error"] = "There was a problem adding a new item. Make sure all the fields are correctly filled";
            }
            //.....

            //CreateItemViewModel newModel = new CreateItemViewModel();
            //newModel.Categories = _categoriesService.GetCategories();

            //return View("Create", newModel);
            return RedirectToAction("Create");

        }

        public IActionResult List()
        {
            var list = _itemsService.ListItems();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var item =_itemsService.GetItem(id);

            if(item == null)
            {
                ViewBag.Error = "Item was not found";
                var list = _itemsService.ListItems();
                return View("List", list);
            }
            else
            {
                return View(item);
            }
        }

        public IActionResult Search(string keyword)
        {
            var list = _itemsService.Search(keyword);
            return View("List", list);
        }

        public IActionResult Delete(int id)
        {
            _itemsService.DeleteItem(id);
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _itemsService.GetItem(id);

            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.CategoryId = item.CategoryId;
            myModel.ImagePath = item.ImagePath;
            myModel.Name = item.Name;
            myModel.Price = item.Price;
            myModel.Stock = item.Stock;


            if (item != null)
            {
                myModel.Categories = _categoriesService.GetCategories();
                return View(myModel);
            }
            else
            {
                return RedirectToAction("List");
            }

        }

        [HttpPost] //this method will be triggered after a Submit button inside a form is clicked
        public IActionResult Edit(int id, CreateItemViewModel data, IFormFile file)
        {
            //to do the comments
            try
            {
                if (file != null)
                {
                    //we get the old path and delete the old image

                    string uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string absolutePath = _hostEnvironment.WebRootPath + @"\Images\" + uniqueFilename;
                    using (var stream = System.IO.File.Create(absolutePath)) //this will create an empty stream at location : absolutePath
                    {
                        file.CopyTo(stream);
                    }
                    data.ImagePath = "/Images/" + uniqueFilename; //the first / indicates asp.net to start locating the image from the root folder
                }
                else
                {
                    //set the original image path if nothing is uploaded
                }


                _itemsService.UpdateItem(id, data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);

                ViewBag.Message = "Item updated successfully";
            }
            catch (Exception ex)
            {
                //ViewBag : a dynamic object, it allows you to declare properties on the fly
                //log the exception
                ViewBag.Error = "There was a problem updating item. make sure all the fields are correctly filled";
            }


            var categories = _categoriesService.GetCategories();
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = categories;

            return View("Edit", myModel);
        }
    }
}
