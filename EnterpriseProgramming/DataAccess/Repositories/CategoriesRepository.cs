using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ShoppingCartContext _context;
        public CategoriesRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetCategories()
        {
            return _context.Categories;
        }
    }
}
