using dyt_ecommerce.Controllers;
using dyt_ecommerce.DataAccess.Entities;

namespace dyt_ecommerce.Models
{
    public abstract class AEntityModelWithFile<TEntity, TModel> : AEntityModelWithImage<TEntity, TModel> 
        where TEntity : AEntity, IEntityWithImage, IEntityWithFile
        where TModel : AEntityModelWithFile<TEntity, TModel>
    {
        public string File { get; set; }

        public override void SetValuesFromEntity(TEntity entity)
        {
            base.SetValuesFromEntity(entity);
            File = entity.File.HasValue ? entity.File.ToString() : null;
        }

        public override void SetUrls(string baseUrl)
        {
            base.SetUrls(baseUrl);

            if(!string.IsNullOrEmpty(File))
            {
                File = string.Format(FileController.FILE_URL, baseUrl, File);
            }
        }
        
    }
}