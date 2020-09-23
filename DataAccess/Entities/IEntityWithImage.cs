using System;

namespace dytsenayasar.DataAccess.Entities
{
    public interface IEntityWithImage
    {
         Guid? Image { get; set; }
    }
}