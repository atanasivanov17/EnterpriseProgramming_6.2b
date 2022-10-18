using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemsService _itemsService;
        public ItemsController(ItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateItemViewModel data)
        {
            try
            {
                _itemsService.AddNewItem(data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);

                ViewBag.Message = "Item added successfully";
            }
            catch (Exception ex)
            {
                //log the exception
                ViewBag.Error = "There was a problem adding a new item. Make sure all the fields are correctly filled";

            }
            //.....

            return View();
        }

        public IActionResult List()
        {
            var list = _itemsService.ListItems();
            return View(list);
        }
    }
}
