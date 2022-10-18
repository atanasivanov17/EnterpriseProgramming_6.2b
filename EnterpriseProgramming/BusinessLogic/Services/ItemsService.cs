using DataAccess.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLogic.ViewModels;

namespace BusinessLogic.Services
{
    public class ItemsService
    {
        private readonly ItemsRepository _itemsRepository;
        public ItemsService(ItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        public void AddNewItem(string name, double price, int categoryId, int stock = 0, string imagePath = null)
        {
            if (_itemsRepository.GetItems().Where(i => i.Name == name).Count() > 0)
                throw new Exception("Item with the same name already exists");

            _itemsRepository.AddItem(new Item()
            {
                CategoryId = categoryId,
                ImagePath = imagePath,
                Name = name,
                Price = price,
                Stock = stock
            });

        }

        public void DeleteItem(int id)
        {
            
        }

        public IQueryable<ItemViewModel> ListItems()
        {
            var list = from i in _itemsRepository.GetItems()
                       select new ItemViewModel()
                       {
                           Id = i.Id,
                           ImagePath = i.ImagePath,
                           Name = i.Name,
                           Price = i.Price,
                           Stock = i.Stock,
                           Category = i.Category.Title
                       };
            
            return list;
        }

        public IQueryable<ItemViewModel> Search(string name)
        {
            return ListItems().Where(i => i.Name.Contains(name));
        }

        public IQueryable<ItemViewModel> Search(string name, double minPrice, double maxPrice)
        {
            return Search(name).Where(i => i.Price >= minPrice && i.Price <= maxPrice);
        }
    }
}
