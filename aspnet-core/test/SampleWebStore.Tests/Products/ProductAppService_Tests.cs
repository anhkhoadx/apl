using Microsoft.EntityFrameworkCore;
using SampleWebStore.Categories;
using SampleWebStore.Categories.Dto;
using SampleWebStore.Products;
using SampleWebStore.Products.Dto;
using SampleWebStore.Shops;
using SampleWebStore.Shops.Dto;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebStore.Tests.Products
{
	public class ProductAppService_Tests : SampleWebStoreTestBase
	{
		private readonly IProductAppService _productAppService;
		private readonly ICategoryAppService _categoryAppService;
		private readonly IShopAppService _shopAppService;

		public ProductAppService_Tests()
		{
			_productAppService = Resolve<IProductAppService>();
			_categoryAppService = Resolve<ICategoryAppService>();
			_shopAppService = Resolve<IShopAppService>();
		}

		[Fact]
		public async Task CreateProduct_Test()
		{
			var name = "Name_" + DateTime.Now.ToString("yyyyMMddHHssfff");
			var description = "Des" + DateTime.Now.ToString("yyyyMMddHHssfff");
			var quantity = 10;
			var price = 100000;
			var categoryName = "Category_" + DateTime.Now.ToString("yyyyMMddHHssfff");
			var dbCategory = await _categoryAppService.CreateAsync(
				new CreateCategoryDto
				{
					Name = categoryName
				});

			var shopName = "Name_" + DateTime.Now.ToString("yyyyMMddHHssfff");
			var shopDes = "Des_" + DateTime.Now.ToString("yyyyMMddHHssfff");

			var dbShop = await _shopAppService.CreateAsync(
				new CreateShopDto
				{
					Name = shopName,
					Description = shopDes
				});

			await _productAppService.CreateAsync(
				new CreateProductDto
				{
					Name = name,
					Description = description,
					Quantity = quantity,
					Price = price,
					CategoryId = dbCategory.Id,
					ShopId = dbShop.Id
				});

			await UsingDbContextAsync(async context =>
			{
				var dbProduct = await context.Products.FirstOrDefaultAsync(p => p.Name == name && p.Description == description);
				dbProduct.ShouldNotBeNull();
				dbProduct.ShopId.ShouldBe(dbShop.Id);
				dbProduct.CategoryId.ShouldBe(dbCategory.Id);
				dbProduct.Quantity.ShouldBe(quantity);
				dbProduct.Price.ShouldBe(price);
			});
		}
	}
}
