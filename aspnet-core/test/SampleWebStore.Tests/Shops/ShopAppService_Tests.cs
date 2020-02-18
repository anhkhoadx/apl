using Microsoft.EntityFrameworkCore;
using SampleWebStore.Shops;
using SampleWebStore.Shops.Dto;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebStore.Tests.Shops
{
	public class ShopAppService_Tests : SampleWebStoreTestBase
	{
		private readonly IShopAppService _shopAppService;

		public ShopAppService_Tests()
		{
			_shopAppService = Resolve<IShopAppService>();
		}

		[Fact]
		public async Task CreateShop_Test()
		{
			var shopName = "Name_" + DateTime.Now.ToString("yyyyMMddHHssfff");
			var shopDes = "Des" + DateTime.Now.ToString("yyyyMMddHHssfff");

			await _shopAppService.CreateAsync(
				new CreateShopDto
				{
					Name = shopName,
					Description = shopDes
				});

			await UsingDbContextAsync(async context =>
			{
				var dbShop = await context.Shops.FirstOrDefaultAsync(s => s.Name == shopName && s.Description == shopDes);
				dbShop.ShouldNotBeNull();
			});
		}
	}
}
