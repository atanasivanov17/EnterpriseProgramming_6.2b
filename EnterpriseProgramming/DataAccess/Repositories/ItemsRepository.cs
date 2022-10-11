using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    class ItemsRepository
    {
        private ShoppingCartContext context;
        public ItemsRepository(ShoppingCartContext _context)
        {
            this.context = _context;
        }
        public void AddItem(Item i)
        {
            context.Items.Add(i);
            context.SaveChanges();
        }

        public void DeleteItem(Item i)
        {
            context.Items.Remove(i);
        }

        public Item GetItem(int id)
        {
            return null;
        }

        public IQueryable<Item> GetItems()
        {
            return null;
        }

        public void UpdateItem(Item originalItem, Item newItem)
        {

        }
    }
}
