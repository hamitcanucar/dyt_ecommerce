using System;

namespace dytsenayasar.DataAccess.Entities
{
    public interface IEntityWithFile
    {
         Guid? File { get; set; }
    }
}