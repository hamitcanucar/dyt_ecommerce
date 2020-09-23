using System;
using dytsenayasar.Controllers;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
     public abstract class AEntityModelWithImage<TEntity, TModel> : AEntityModel<TEntity, TModel> 
        where TEntity : AEntity, IEntityWithImage 
        where TModel : AEntityModelWithImage<TEntity, TModel>
    {
        public string Image { get; set; }
        public string Thumbnail { get; set; }

        public override void SetValuesFromEntity(TEntity entity)
        {
            base.SetValuesFromEntity(entity);
            Image = entity.Image.HasValue ? entity.Image.ToString() : null;
        }

        public virtual void SetValuesFromEntity(string baseUrl, TEntity entity)
        {
            SetValuesFromEntity(entity);
            SetUrls(baseUrl);
        }

        public virtual void SetUrls(string baseUrl)
        {
            var img = Image;

            if(!string.IsNullOrEmpty(img))
            {
                Image = String.Format(FileController.IMAGE_URL, baseUrl, img);
            }
        }

    }
}