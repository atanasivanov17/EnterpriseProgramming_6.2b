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

                ViewBag.Message = "Item added successfully";
            }
            catch (Exception ex)
            {
                //log the exception
                ViewBag.Error = "There was a problem adding a new item. Make sure all the fields are correctly filled";
                throw;
            }
            //.....

            return View();
        }
    }
}
