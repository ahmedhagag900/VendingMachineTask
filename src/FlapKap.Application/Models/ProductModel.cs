﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Models
{
    public class ProductModel: BaseModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public double Price { get; set; }
        public int AvailableAmount { get; set; }
        public int SellerId { get; set; }
    }
}
