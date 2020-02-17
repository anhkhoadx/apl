using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Validation;
using SampleWebStore.Categories;
using SampleWebStore.Shops;
using System.ComponentModel.DataAnnotations;

namespace SampleWebStore.Products.Dto
{
	[AutoMapTo(typeof(Product))]
	public class CreateProductDto : EntityDto<long>, ICustomValidate
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

		public long ShopId { get; set; }

		public int CategoryId { get; set; }

		public void AddValidationErrors(CustomValidationContext context)
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				context.Results.Add(new ValidationResult("Product name shouldn't be empty"));
			}

			var shopRepo = IocManager.Instance.Resolve<IRepository<Shop, long>>();
			var categoryRepo = IocManager.Instance.Resolve<IRepository<Category, int>>();

			if (shopRepo.FirstOrDefault(ShopId) == null)
			{
				context.Results.Add(new ValidationResult("Invalid product shop"));
			}

			if (categoryRepo.FirstOrDefault(CategoryId) == null)
			{
				context.Results.Add(new ValidationResult("Invalid product category"));
			}

			if (Quantity < 0)
			{
				context.Results.Add(new ValidationResult("Product quantity shouldn't be a negative number"));
			}

			if (Price < 0)
			{
				context.Results.Add(new ValidationResult("Product price shouldn't be a negative number"));
			}
		}
	}
}