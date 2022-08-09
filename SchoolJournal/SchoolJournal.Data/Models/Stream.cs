using System.ComponentModel.DataAnnotations;

namespace SchoolJournal.Models.DB
{
    public class Stream
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [DataType(DataType.Date)]
        [Display(Name ="Дата начала")]
        public DateTime Started { get; set; }
        [Display(Name = "Класс")]
        public int CurrentClass { get; set; }
        [Display(Name = "Поток закрыт")]
        public bool IsCompleted { get; set; } = false;
        List<Pupil> Pupils { get; set; } = new List<Pupil>();
    }
}
