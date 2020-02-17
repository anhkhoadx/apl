using Abp.Application.Services.Dto;

namespace SampleWebStore.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

