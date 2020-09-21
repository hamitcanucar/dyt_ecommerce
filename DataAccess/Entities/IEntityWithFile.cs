using System;

namespace dyt_ecommerce.DataAccess.Entities
{
    public interface IEntityWithFile
    {
         Guid? File { get; set; }
    }
}