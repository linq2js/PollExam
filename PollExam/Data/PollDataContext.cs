using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Linq;
using System.Data.SqlServerCe;
using System.Linq;
using System.Linq.Expressions;
using PollExam.Entities;

namespace PollExam.Data
{
    [Export(typeof(IQuery))]
    [Export(typeof(IRepository<PollOptionEntity>))]
    [Export(typeof(IRepository<PollEntity>))]
    [Export(typeof(IRepository<VoteEntity>))]
    internal class PollDataContext : IQuery, IRepository<PollOptionEntity>, IRepository<PollEntity>, IRepository<VoteEntity>
    {
        [Import("ConnectionString")]
        public String ConnectionString { get; set; }

        private DataContext _readContext;

        private DataContext GetReadContext()
        {
            return _readContext ?? (_readContext = new DataContext(new SqlCeConnection(ConnectionString)));
        }

        private DataContext GetWriteContext()
        {
            return new DataContext(new SqlCeConnection(ConnectionString));
        }

        private void InternalSave<TEntity>(params TEntity[] entities) where TEntity : class, IEntity
        {
            var writeContext = GetWriteContext();
            var table = writeContext.GetTable<TEntity>();
            foreach (var entity in entities)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                    table.InsertOnSubmit(entity);
                }
                else
                {
                    var oldVersion = new List<TEntity>();
                    Activator.CreateInstance(typeof (FindEntityByKey<>).MakeGenericType(typeof (TEntity)), table, entity.Id, oldVersion);
                    if (oldVersion.Count > 0)
                    {
                        table.Attach(entity, oldVersion[0]);
                    }
                    else
                    {
                        table.InsertOnSubmit(entity);
                    }
                }
            }
            writeContext.SubmitChanges();
        }

        public IQueryable<TEntity> Create<TEntity>() where TEntity : class, IEntity
        {
            return GetReadContext().GetTable<TEntity>();
        }

        public void Save(params PollOptionEntity[] entities)
        {
            InternalSave(entities);
        }

        public void Save(params PollEntity[] entities)
        {
            InternalSave(entities);
        }

        public void Save(params VoteEntity[] entities)
        {
            InternalSave(entities);
        }
    }

    internal class FindEntityByKey<TEntity> where TEntity : IEntity
    {
        public FindEntityByKey(IQueryable<TEntity> queryable, Guid id, IList<TEntity> result)
        {
            var entity = queryable.FirstOrDefault(GetFindByIdPredicate<TEntity>(id));
            if (!Equals(entity, null)) result.Add(entity);
        }

        private static Expression<Func<T, Boolean>> GetFindByIdPredicate<T>(Guid id)
        {
            var param = Expression.Parameter(typeof(T));
            var idGetter = Expression.PropertyOrField(param, "Id");
            var compare = Expression.Equal(idGetter, Expression.Constant(id));
            var lambda = Expression.Lambda(compare, param);
            return (Expression<Func<T, Boolean>>)lambda;
        }
    }
}
