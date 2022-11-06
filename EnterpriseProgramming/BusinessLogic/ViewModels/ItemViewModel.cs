using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string  Category { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
    }
}
