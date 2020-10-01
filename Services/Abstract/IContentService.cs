using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Models;

namespace dytsenayasar.Services.Abstract
{
    public interface IContentService
    {
        Task<Content> Get(Guid id);
        Task<Content> GetUserContent(Guid id, Guid userId);
        Task<ICollection<Guid>> GetAllContentOwnerId(Guid contentId);
        Task<Content> Delete(Guid id);
        Task<ICollection<Content>> GetAllContent(int limit = 20, int offset = 0);
        Task<long> GetContentCount();
    }
}