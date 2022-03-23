using System.ComponentModel.DataAnnotations;

namespace Books.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}