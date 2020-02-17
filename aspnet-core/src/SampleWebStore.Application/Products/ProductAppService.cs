using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using SampleWebStore.Authorization;
using SampleWebStore.Pagination;
using SampleWebStore.Products.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebStore.Products
{
	[AbpAuthorize(PermissionNames.PagesShops)]
	public class ProductAppService : AsyncCrudAppService<Product, ProductDto, long, PagedProductResultRequestDto, CreateProductDto, CreateProductDto>, IProductAppService
	{
		public ProductAppService(IRepository<Product, long> repository)
			: base(repository)
		{
		}

		public override async Task<ProductDto> CreateAsync(CreateProductDto input)
		{
			CheckCreatePermission();

			var checkingProduct = await Repository.FirstOrDefaultAsync(s => s.ShopId == input.ShopId &&
			                                                                input.Name.ToLower().Equals(s.Name.ToLower()));

			if (checkingProduct != null)
			{
				throw new UserFriendlyException("Duplicated product name");
			}

			var product = ObjectMapper.Map<Product>(input);
			product.ShopId = input.ShopId;
			product.CategoryId = input.CategoryId;

			await Repository.InsertAsync(product);
			CurrentUnitOfWork.SaveChanges();

			return MapToEntityDto(product);
		}

		public override async Task<ProductDto> UpdateAsync(CreateProductDto input)
		{
			CheckUpdatePermission();
			var dbProduct = await Repository.GetAsync(input.Id);

			if (dbProduct == null)
			{
				throw new UserFriendlyException("Product is not existed");
			}

			if (dbProduct.ShopId != input.ShopId)
			{
				throw new UserFriendlyException("Action can only be done by shop owner");
			}

			var checkingProduct = Repository.FirstOrDefault(s => s.Id != input.Id &&
			                                                  s.Name.ToLower().Equals(input.Name.ToLower()));

			if (checkingProduct != null)
			{
				throw new UserFriendlyException("Duplicated product name");
			}

			MapToEntity(input, dbProduct);
			await Repository.UpdateAsync(dbProduct);

			return MapToEntityDto(dbProduct);
		}

		protected override IQueryable<Product> CreateFilteredQuery(PagedProductResultRequestDto input)
		{
			return Repository.GetAll().Where(s => s.ShopId == input.ShopId).WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
				x => x.Name.Contains(input.Keyword));
		}
	}
}
