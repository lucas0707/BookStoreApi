using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreApi.Models
{
    public class Book
    {
        [Key]
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
        public double Preco { get; set; }
    }
}