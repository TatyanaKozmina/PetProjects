using System.ComponentModel.DataAnnotations;

namespace SchoolJournal.Models.DB
{
    public class Pupil
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Имя")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Почта")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Поток")]
        public Guid StreamId { get; set; }

        public Stream Stream { get; set; }

        public List<JournalRecord> JournalRecords { get; set; } = new List<JournalRecord>();

    }
}
