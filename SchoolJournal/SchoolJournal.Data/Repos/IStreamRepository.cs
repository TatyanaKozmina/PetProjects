namespace SchoolJournal.Data.Repos
{
    public interface IStreamRepository
    {
        Task<List<Models.DB.Stream>> GetAll();
        Task Create(Models.DB.Stream stream);
        Task<Models.DB.Stream> GetById(Guid? id);
        Task Update(Models.DB.Stream stream);
        Task Delete(Guid id);
    }
}
