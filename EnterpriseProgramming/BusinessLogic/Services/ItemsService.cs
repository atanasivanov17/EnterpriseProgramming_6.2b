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
            var item = _itemsRepository.GetItems().SingleOrDefault(x => x.Id == id);
            if(item != null)
                _itemsRepository.DeleteItem(item);

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
                           Category = i.Category.Title,
                           CategoryId = i.Category.Id
                       };
            
            return list;
        }

        public ItemViewModel GetItem(int id)
        {
            return ListItems().SingleOrDefault(i => i.Id == id);
        }

        public IQueryable<ItemViewModel> Search(string name)
        {
            return ListItems().Where(i => i.Name.Contains(name));
        }

        public IQueryable<ItemViewModel> Search(string name, double minPrice, double maxPrice)
        {
            return Search(name).Where(i => i.Price >= minPrice && i.Price <= maxPrice);
        }

        public IQueryable<ItemViewModel> SearchSortByPrice(string name, double minPrice, double maxPrice, bool ascending)
        {
            return ascending ? Search(name, minPrice, maxPrice).OrderBy(x => x.Price) :
                   Search(name, minPrice, maxPrice).OrderByDescending(x => x.Price);
        }

        public void UpdateItem(int id, string name, double price, int categoryId, int stock = 0, string imagePath = null)
        {
            var item = _itemsRepository.GetItems().SingleOrDefault(x => x.Id == id);
            if (item != null)
                _itemsRepository.Update(item, new Domain.Models.Item()
                {
                    CategoryId = categoryId,
                    Id = id,
                    Name = name,
                    Price = price,
                    ImagePath = imagePath,
                    Stock = stock
                });
        }

    }
}
