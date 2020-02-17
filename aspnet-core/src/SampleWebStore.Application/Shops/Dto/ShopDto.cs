using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace SampleWebStore.Shops.Dto
{
	[AutoMapFrom(typeof(Shop))]
	public class ShopDto : EntityDto<long>, IHasCreationTime, IHasModificationTime, ISoftDelete
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string Address { get; set; }

		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }

		public bool IsDeleted { get; set; }
	}
}
