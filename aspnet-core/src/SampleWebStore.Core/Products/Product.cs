using System;
using Abp.Domain.Entities.Auditing;
using SampleWebStore.Categories;
using SampleWebStore.Shops;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace SampleWebStore.Products
{
	public class Product : Entity<long>, IHasCreationTime, IHasModificationTime, ISoftDelete
	{
		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Product quantity shouldn't be a negative number}")]
		public int Quantity { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "Product price shouldn't be a negative number}")]
		public double Price { get; set; }
	
		public bool IsDeleted { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }

		[ForeignKey("Shop")]
		public long ShopId { get; set; }

		public virtual Shop Shop { get; set; }

		[ForeignKey("Category")]
		public int CategoryId { get; set; }

		public virtual Category Category { get; set; }
	}
}
