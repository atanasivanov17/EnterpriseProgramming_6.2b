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

        
    }
}
