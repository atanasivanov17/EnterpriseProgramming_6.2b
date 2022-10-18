using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class ItemsRepository
    {
        private readonly ShoppingCartContext _context;
        public ItemsRepository(ShoppingCartContext context)
        {
            _context = context;
        }
        public void AddItem(Item i)
        {
            _context.Items.Add(i);
            _context.SaveChanges();
        }

        public void DeleteItem(Item i)
        {
            _context.Items.Remove(i);
            _context.SaveChanges();
        }

        public Item GetItem(int id)
        {
            return null;
        }

        public IQueryable<Item> GetItems()
        {
            return _context.Items;
        }

        public void UpdateItem(Item originalItem, Item newItem)
        {

        }
    }
}
