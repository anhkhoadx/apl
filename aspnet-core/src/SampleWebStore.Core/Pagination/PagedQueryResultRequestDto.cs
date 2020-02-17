using Abp.Application.Services.Dto;

namespace SampleWebStore.Pagination
{
	public class PagedQueryResultRequestDto : PagedResultRequestDto
	{
		public string Keyword { get; set; }
	}
}
