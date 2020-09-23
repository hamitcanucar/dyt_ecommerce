using System.Linq;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Util
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> ToPage<TEntity>(this IQueryable<TEntity> query, int limit, int offset) where TEntity : AEntity
        {
            return query.OrderBy(x => x.CreateTime).ToPageWithoutOrder(limit, offset);
        }

        public static IQueryable<TEntity> ToPageWithoutOrder<TEntity>(this IQueryable<TEntity> query, int limit, int offset) where TEntity : AEntity
        {
            return query.Skip(limit * offset).Take(limit);
        }
    }
}