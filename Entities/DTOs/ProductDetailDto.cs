using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
   public class ProductDetailDto:IDto
    {
        //Data Transformation Object

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }
    }
}
