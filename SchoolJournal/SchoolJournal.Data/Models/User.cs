using System.ComponentModel.DataAnnotations;

namespace SchoolJournal.Models.DB
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public Role Role { get; set; }
    }
}
