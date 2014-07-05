namespace PollExam.Data
{
    public interface IRepository<in TEntity>
    {
        void Save(params TEntity[] entities);
    }
}
