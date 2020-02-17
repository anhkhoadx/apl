using Abp.Application.Services;
using SampleWebStore.Products.Dto;

namespace SampleWebStore.Products
{
	public interface IProductAppService : IAsyncCrudAppService<ProductDto, long, PagedProductResultRequestDto, CreateProductDto, CreateProductDto>
	{
	}
}
