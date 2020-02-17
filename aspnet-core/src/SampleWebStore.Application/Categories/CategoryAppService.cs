using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using SampleWebStore.Authorization;
using SampleWebStore.Categories.Dto;
using SampleWebStore.Pagination;
using System.Linq;

namespace SampleWebStore.Categories
{
	[AbpAuthorize(PermissionNames.PagesShops)]
	public class CategoryAppService : AsyncCrudAppService<Category, CategoryDto, int, PagedQueryResultRequestDto, CreateCategoryDto, CreateCategoryDto>, ICategoryAppService
	{
		public CategoryAppService(IRepository<Category, int> repository)
			: base(repository)
		{
		}
		
		protected override IQueryable<Category> CreateFilteredQuery(PagedQueryResultRequestDto input)
		{
			return Repository.GetAll().WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
				x => x.Name.Contains(input.Keyword));
		}
	}
}
