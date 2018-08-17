using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o título do livro.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Digite o nome do autor.")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Digite o nome da editora.")]
        public string Publisher { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float BookPrice { get; set; }
    }
}