using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using SampleWebStore.Authorization;
using SampleWebStore.Categories.Dto;
using SampleWebStore.Pagination;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;

namespace SampleWebStore.Categories
{
	[AbpAuthorize(PermissionNames.PagesShops)]
	public class CategoryAppService : AsyncCrudAppService<Category, CategoryDto, int, PagedQueryResultRequestDto, CreateCategoryDto, CreateCategoryDto>, ICategoryAppService
	{
		public CategoryAppService(IRepository<Category, int> repository)
			: base(repository)
		{
		}

		public override async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
		{
			if (await Repository.FirstOrDefaultAsync(c => c.Name.ToLower().Equals(input.Name.ToLower())) != null)
			{
				throw new UserFriendlyException("Duplicate category name");
			}

			return await base.CreateAsync(input);
		}

		public override async Task<CategoryDto> UpdateAsync(CreateCategoryDto input)
		{
			if (await Repository.FirstOrDefaultAsync(c => c.Id != input.Id && c.Name.ToLower().Equals(input.Name.ToLower())) != null)
			{
				throw new UserFriendlyException("Duplicate category name");
			}

			return await base.UpdateAsync(input);
		}

		protected override IQueryable<Category> CreateFilteredQuery(PagedQueryResultRequestDto input)
		{
			return Repository.GetAll().WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
				x => x.Name.Contains(input.Keyword));
		}
	}
}
