using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace SampleWebStore.Products.Dto
{
	[AutoMapFrom(typeof(Product))]
	public class ProductDto : EntityDto<long>, IHasCreationTime, IHasModificationTime, ISoftDelete
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

		public long ShopId { get; set; }

		public int CategoryId { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }

		public bool IsDeleted { get; set; }
	}
}
