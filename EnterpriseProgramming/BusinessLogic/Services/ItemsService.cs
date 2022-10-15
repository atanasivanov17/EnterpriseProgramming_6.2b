using DataAccess.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
    }
}
