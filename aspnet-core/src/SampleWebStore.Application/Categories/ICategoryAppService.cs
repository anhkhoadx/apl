using Abp.Application.Services;
using SampleWebStore.Categories.Dto;
using SampleWebStore.Pagination;

namespace SampleWebStore.Categories
{
	public interface ICategoryAppService : IAsyncCrudAppService<CategoryDto, int, PagedQueryResultRequestDto, CreateCategoryDto, CreateCategoryDto>
	{
	}
}
