using Abp.Application.Services;
using SampleWebStore.Pagination;
using SampleWebStore.Shops.Dto;

namespace SampleWebStore.Shops
{
	public interface IShopAppService : IAsyncCrudAppService<ShopDto, long, PagedQueryResultRequestDto, CreateShopDto, CreateShopDto>
	{
	}
}
