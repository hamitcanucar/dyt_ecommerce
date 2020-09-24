using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Services.Abstract
{
    public interface IContentDeliveryService
    {
        Task<ICollection<UserContent>> GetAllContents(Guid userId, string search = null);
        Task<ICollection<Guid>> GetAllContentOwnersId(Guid contentId);
        Task<bool> CheckContentFileAvailableForUser(Guid fileId, Guid requestingUserId);
        Task<ICollection<Guid>> CheckAvailableContentFiles(Guid userId, ICollection<Guid> contentIds);
    }
}