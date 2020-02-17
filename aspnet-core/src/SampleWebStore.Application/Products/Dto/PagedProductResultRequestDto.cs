using Abp.Application.Services.Dto;

namespace SampleWebStore.Products.Dto
{
	public class PagedProductResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long ShopId { get; set; }
    }
}
