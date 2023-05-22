using System.ComponentModel.DataAnnotations;

namespace Avans.Demo.Domain
{
    public class ApiBook
    {
        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int Pages { get; set; }

        public string? Website { get; set; }
    }
}
