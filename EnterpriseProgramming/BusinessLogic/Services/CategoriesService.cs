using BusinessLogic.ViewModels;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class CategoriesService
    {
        private readonly ICategoriesRepository _cr;
        public CategoriesService(ICategoriesRepository cr)
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
