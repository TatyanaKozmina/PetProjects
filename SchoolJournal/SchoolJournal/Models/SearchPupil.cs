namespace SchoolJournal.Models
{
    public class SearchPupil
    {
        public string SearchText { get; set; } = string.Empty;  
        public List<DB.Pupil> Pupils = new List<DB.Pupil>();
    }
}
