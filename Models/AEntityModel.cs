using System;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
   public abstract class AEntityModel<TEntity, TModel> where TEntity : AEntity
    {
        public Guid ID { get; set; }
        public virtual void SetValuesFromEntity(TEntity entity)
        {
            ID = entity.ID;
        }
        public abstract TEntity ToEntity();
    }
}