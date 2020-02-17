using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace SampleWebStore.Categories.Dto
{
	[AutoMapTo(typeof(Category))]
	public class CreateCategoryDto : EntityDto<int>, ICustomValidate
	{
		public string Name { get; set; }

		public void AddValidationErrors(CustomValidationContext context)
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				context.Results.Add(new ValidationResult("Product name shouldn't be empty"));
			}
		}
	}
}
