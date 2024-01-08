using DataAccess.Abstract;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
        where TEntity : class,new()
        where TContext : DbContext, new()

    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using bittiği anda bellekten atılacak
            using (TContext context = new TContext())
            {
                //referansı yakala
                var addedEntity = context.Entry(entity);
                //ekle
                addedEntity.State = EntityState.Added;
                //değişiklikleri kaydet
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using bittiği anda bellekten atılacak
            using (TContext context = new TContext())
            {
                //referansı yakala
                var deletedEntity = context.Entry(entity);
                //sil
                deletedEntity.State = EntityState.Deleted;
                //değişiklikleri kaydet
                context.SaveChanges();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            //IDisposable pattern implementation of c#
            //using bittiği anda bellekten atılacak
            using (TContext context = new TContext())
            {
                //filtre null ise tümünü getir
                //değilse filtreyi uygula
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            //IDisposable pattern implementation of c#
            //using bittiği anda bellekten atılacak
            using (TContext context = new TContext())
            {
                //filtreyi uygula
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public void Update(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using bittiği anda bellekten atılacak
            using (TContext context = new TContext())
            {
                //referansı yakala
                var updatedEntity = context.Entry(entity);
                //güncelle
                updatedEntity.State = EntityState.Modified;
                //değişiklikleri kaydet
                context.SaveChanges();
            }
        }

    }
}
