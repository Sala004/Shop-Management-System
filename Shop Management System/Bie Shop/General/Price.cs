﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bie_Shop.General
{
    public class Price
    {
        public double itemPrice {  get; set; }
        public Currency Currency { get; set; }

        public override string ToString()
        {
            return $"{itemPrice} {Currency}";
        }
    }
}
