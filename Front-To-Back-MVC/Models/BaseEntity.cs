using System;
namespace Front_To_Back_MVC.Models
{
	public class BaseEntity
	{
		public int Id { get; set; }
		public bool IsDeleted { get; set; } = false;
		public DateTime CreateDate { get; set; } = DateTime.Now;
	}
}

