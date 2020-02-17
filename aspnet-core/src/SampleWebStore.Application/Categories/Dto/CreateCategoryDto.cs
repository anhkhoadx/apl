using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace SampleWebStore.Categories.Dto
{
	[AutoMapTo(typeof(Category))]
	public class CreateCategoryDto : EntityDto<int>
	{
		public string Name { get; set; }
	}
}
