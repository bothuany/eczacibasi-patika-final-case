﻿using StockTrackingApp.Business.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Business.Dto.Brand
{
    public class GetBrandByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
