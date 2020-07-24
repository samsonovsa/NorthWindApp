using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.ConfigOptions
{
    public class ProductOptions
    {
        public const string Products = nameof(Products);

        public int MaxCountOnPage { get; set; }
    }
}
