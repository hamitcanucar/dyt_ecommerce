using System;

namespace dyt_ecommerce.DataAccess.Entities
{
    public interface IEntityWithImage
    {
         Guid? Image { get; set; }
    }
}