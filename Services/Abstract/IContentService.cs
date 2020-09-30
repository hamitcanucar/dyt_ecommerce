using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Models;

namespace dytsenayasar.Services.Abstract
{
    public interface IContentService
    {
        Task<Content> Create(ContentModel model);
        Task<Content> Update(Guid id, ContentModel model);
        Task<Content> Get(Guid id);
        Task<Content> GetUserContent(Guid id, Guid userId);
        Task<ICollection<Guid>> GetAllContentOwnerId(Guid contentId);
        Task<bool> CheckContentFileAvailableForUser(Guid fileId, Guid requestingUserId);
        Task<long> FindCount(ContentFindParametersModel parameters);
        Task<Content> Delete(Guid id);
        Task<ICollection<Content>> GetAllContent(int limit = 20, int offset = 0);
        Task<long> GetContentCount();
    }
}