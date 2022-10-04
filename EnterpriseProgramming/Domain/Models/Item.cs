﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
    }
}
