using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Models;

namespace dytsenayasar.Services.Abstract
{
    public interface IContentService
    {
        Task<Content> Create(ContentModel model, Guid creatorId);
        Task<Content> Update(Guid id, ContentModel model);
        Task<int> CreateCategories(ICollection<Category> categories);
        Task<Content> AddCategory(Guid contentId, ICollection<int> categoryIds);
        Task<Content> DeleteCategory(Guid contentId, ICollection<int> categoryIds);
        Task<int> RemoveCategories(ICollection<int> categories);
        Task<bool> UpdateFileNames(Content content, Guid? file);
        Task<Content> Get(Guid id);
        Task<ICollection<Category>> GetAllCategories();
        Task<Content> GetUserContent(Guid id, Guid userId);
        Task<ICollection<Content>> Find(ContentFindParametersModel parameters, int limit = 20, int offset = 0);
        Task<long> FindCount(ContentFindParametersModel parameters);
        Task<Content> Delete(Guid id);
        Task<ICollection<Content>> GetAllContent(int limit = 20, int offset = 0);
        Task<long> GetContentCount();
    }
}