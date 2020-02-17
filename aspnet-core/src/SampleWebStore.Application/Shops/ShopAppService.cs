using System.Linq;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using SampleWebStore.Pagination;
using SampleWebStore.Shops.Dto;
using System.Threading.Tasks;
using SampleWebStore.Authorization;
using Abp.Linq.Extensions;
using Abp.Extensions;

namespace SampleWebStore.Shops
{
	[AbpAuthorize(PermissionNames.PagesShops)]
	public class ShopAppService : AsyncCrudAppService<Shop, ShopDto, long, PagedQueryResultRequestDto, CreateShopDto, CreateShopDto>, IShopAppService
	{
		private readonly IAbpSession _abpSession;

		public ShopAppService(IRepository<Shop, long> repository, IAbpSession abpSession)
			: base(repository)
		{
			_abpSession = abpSession;
		}

		public override async Task<ShopDto> CreateAsync(CreateShopDto input)
		{
			long userId = GetUserId();
			CheckCreatePermission();

			var checkingShop = await Repository.FirstOrDefaultAsync(s =>
				input.Name.ToLower().Equals(s.Name.ToLower()));

			if (checkingShop != null)
			{
				throw new UserFriendlyException("Duplicated shop name");
			}

			var shop = ObjectMapper.Map<Shop>(input);
			shop.UserId = userId;

			await Repository.InsertAsync(shop);
			CurrentUnitOfWork.SaveChanges();

			return MapToEntityDto(shop);
		}

		public override async Task<ShopDto> UpdateAsync(CreateShopDto input)
		{
			long userId = GetUserId();
			CheckUpdatePermission();
			var dbShop = await Repository.GetAsync(input.Id);

			if (dbShop == null)
			{
				throw new UserFriendlyException("Shop is not existed");
			}

			if (dbShop.UserId != userId)
			{
				throw new UserFriendlyException("Action can only be done by shop owner");
			}

			var checkingShop = Repository.FirstOrDefault(s => s.Id != input.Id &&
			                                                  s.Name.ToLower().Equals(input.Name.ToLower()));

			if (checkingShop != null)
			{
				throw new UserFriendlyException("Duplicated shop name");
			}
			
			MapToEntity(input, dbShop);
			await Repository.UpdateAsync(dbShop);

			return MapToEntityDto(dbShop);
		}

		protected override IQueryable<Shop> CreateFilteredQuery(PagedQueryResultRequestDto input)
		{
			var userId = GetUserId();

			return Repository.GetAll().Where(s => s.UserId == userId).WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
				x => x.Name.Contains(input.Keyword) || x.Name.Contains(input.Keyword) ||
				     x.Description.Contains(input.Keyword) || x.Address.Contains(input.Keyword));
		}

		private long GetUserId()
		{
			if (_abpSession.UserId == null)
			{
				throw new UserFriendlyException("Please log in to continue");
			}

			return _abpSession.UserId.Value;
		}
	}
}
