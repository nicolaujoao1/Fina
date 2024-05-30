using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage ="Titulo inválido")]
        [MaxLength(80, ErrorMessage ="O titulo deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Descrição inválida")]
        public string? Description { get; set; }
    }
}
