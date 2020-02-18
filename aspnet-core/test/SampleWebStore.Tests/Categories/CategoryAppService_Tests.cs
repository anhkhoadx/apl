using Microsoft.EntityFrameworkCore;
using SampleWebStore.Categories;
using SampleWebStore.Categories.Dto;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebStore.Tests.Categories
{
	public class CategoryAppService_Tests : SampleWebStoreTestBase
	{
		private readonly ICategoryAppService _categoryAppService;

		public CategoryAppService_Tests()
		{
			_categoryAppService = Resolve<ICategoryAppService>();
		}

		[Fact]
		public async Task CreateCategory_Test()
		{
			var categoryName = "Category_" + DateTime.Now.ToString("yyyyMMddHHssfff");
			await _categoryAppService.CreateAsync(
				new CreateCategoryDto
				{
					Name = categoryName
				});

			await UsingDbContextAsync(async context =>
			{
				var dbCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
				dbCategory.ShouldNotBeNull();
			});
		}
	}
}
