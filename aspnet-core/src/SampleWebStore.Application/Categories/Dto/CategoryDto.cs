using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace SampleWebStore.Categories.Dto
{
	[AutoMapFrom(typeof(Category))]
	public class CategoryDto : EntityDto<int>, IHasCreationTime, IHasModificationTime, ISoftDelete
	{
		public string Name { get; set; }
		
		public DateTime CreationTime { get; set; }

		public DateTime? LastModificationTime { get; set; }

		public bool IsDeleted { get; set; }
	}
}
