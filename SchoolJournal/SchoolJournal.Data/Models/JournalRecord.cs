using System.ComponentModel.DataAnnotations;

namespace SchoolJournal.Models.DB
{
    public class JournalRecord
    {   
        public Guid Id { get; set; } = Guid.NewGuid();

        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        [Required]
        public DateTime Created { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Предмет")]
        public Subject Subject { get; set; }

        [Required]
        [Range(2, 5,
        ErrorMessage = "Оценки могут быть в диапазоне {1} и {2}.")]
        [Display(Name = "Оценка")]
        public int Mark { get; set; }

        [Display(Name = "Комментарий")]
        public string? Comment { get; set; }

        [Required]
        public Guid PupilId { get; set; }
        public Pupil Pupil { get; set; }
    }
}
