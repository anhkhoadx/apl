using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace SampleWebStore.Shops.Dto
{
	[AutoMapTo(typeof(Shop))]
	public class CreateShopDto : EntityDto<long>, ICustomValidate
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string Address { get; set; }

		public void AddValidationErrors(CustomValidationContext context)
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				context.Results.Add(new ValidationResult("Shop name shouldn't be empty"));
			}

			if (string.IsNullOrWhiteSpace(Description))
			{
				context.Results.Add(new ValidationResult("Product description shouldn't be empty"));
			}
		}
	}
}
