using BusinessLogic.ViewModels;
using DataAccess.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class CategoriesService
    {
        private readonly CategoriesRepository _cr;
        public CategoriesService(CategoriesRepository cr)
        {
            _cr = cr;
        }

        public IQueryable<CategoryViewModel> GetCategories()
        {
            var list = from c in _cr.GetCategories()
                       select new CategoryViewModel()
                       {
                           Id = c.Id,
                           Title = c.Title
                       };

            return list;
        }
    }
}
