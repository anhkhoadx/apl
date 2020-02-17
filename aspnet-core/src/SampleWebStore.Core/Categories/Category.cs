using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebStore.Categories
{
	public class Category : Entity<int>, IHasCreationTime, IHasModificationTime, ISoftDelete
	{
		[Required]
		public string Name { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }

		public bool IsDeleted { get; set; }
	}
}
