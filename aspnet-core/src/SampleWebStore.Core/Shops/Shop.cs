using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SampleWebStore.Authorization.Users;
using SampleWebStore.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleWebStore.Shops
{
	public class Shop : Entity<long>, IHasCreationTime, IHasModificationTime, ISoftDelete
	{
		/// <summary>
		/// Shop name is unique in this practice
		/// </summary>
		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		/// <summary>
		/// Empty if it's an online shop
		/// </summary>
		public string Address { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }

		[ForeignKey("User")]
		public long UserId { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}
