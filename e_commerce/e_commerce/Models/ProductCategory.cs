using System;
using Microsoft.CodeAnalysis;

namespace e_commerce.Models
{
	public class ProductCategory
	{
		
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    
	}
}

