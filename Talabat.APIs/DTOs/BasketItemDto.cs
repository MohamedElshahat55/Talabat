﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		[Range(0.1,double.MaxValue,ErrorMessage ="Price Must be Greater Than Zero ")]
		public decimal Price { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Category { get; set; }
		[Required]
		[Range(0.1, int.MaxValue, ErrorMessage = "Quantity Must be At least One! ")]
		public int Quantity { get; set; }
	}
}