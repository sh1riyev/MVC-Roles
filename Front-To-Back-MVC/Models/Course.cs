﻿using System;
namespace Front_To_Back_MVC.Models
{
	public class Course :BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public ICollection<CourseImage> ? Images { get; set; }
	}
}

