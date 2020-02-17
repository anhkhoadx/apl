using System.ComponentModel.DataAnnotations;

namespace SampleWebStore.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}