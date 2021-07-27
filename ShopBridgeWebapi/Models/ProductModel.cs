﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridgeWebapi.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string ManufacturedBY { get; set; }
        public int Stock { get; set; }
    }
}