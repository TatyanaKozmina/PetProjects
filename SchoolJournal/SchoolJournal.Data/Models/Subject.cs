using System.ComponentModel.DataAnnotations;
namespace SchoolJournal.Models.DB
{
    public enum Subject
    {
        [Display(Name = "Математика")]
        Math,
        [Display(Name = "Чтение")]
        Reading,
        [Display(Name = "Письмо")]
        Writing,
        [Display(Name = "Физкультура")]
        Sport
    }
}
