using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace SampleWebStore.Shops.Dto
{
	[AutoMapTo(typeof(Shop))]
	public class CreateShopDto : EntityDto<long>
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string Address { get; set; }
	}
}
