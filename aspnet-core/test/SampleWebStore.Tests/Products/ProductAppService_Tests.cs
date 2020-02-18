using SampleWebStore.Products;
using Xunit;

namespace SampleWebStore.Tests.Categories
{
	public class ProductAppService_Tests : SampleWebStoreTestBase
	{
		private readonly IProductAppService _productAppService;

		public ProductAppService_Tests()
		{
			_productAppService = Resolve<IProductAppService>();
		}

		[Fact]
		public void CreateProduct_Test()
		{
			
		}
	}
}
