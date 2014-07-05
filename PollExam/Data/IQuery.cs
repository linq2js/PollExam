using System.Linq;
using PollExam.Entities;

namespace PollExam.Data
{
    public interface IQuery
    {
        IQueryable<TEntity> Create<TEntity>() where TEntity : class, IEntity;
    }
}
