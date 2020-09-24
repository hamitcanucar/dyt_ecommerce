using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dytsenayasar.DataAccess;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Models;
using dytsenayasar.Services.Abstract;
using dytsenayasar.Util;
using dytsenayasar.Context;

namespace dytsenayasar.Services.Concrete
{
    public class ContentService : IContentService
    {
        private readonly ApplicationDbContext _context;

        public ContentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Content> Create(ContentModel model, Guid creatorId)
        {
            var content = model.ToEntity();

            content.CreatorId = creatorId;
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task<Content> Delete(Guid id)
        {
            var content = await _context.Contents.FindAsync(id);
            return await Delete(content);
        }


        public async Task<Content> Update(Guid id, ContentModel model)
        {
            var content = await GetContentQuery(id)
                .FirstOrDefaultAsync();

            return await Update(content, model);
        }

        public async Task<Content> AddCategory(Guid contentId, ICollection<int> categoryIds)
        {
            var content = new Content { ID = contentId };

            if (!(await CheckContentExist(contentId)))
            {
                return null;
            }

            return await AddCategory(content, categoryIds);
        }

        private async Task<Content> AddCategory(Content content, ICollection<int> categoryIds)
        {
            HashSet<int> set = new HashSet<int>(categoryIds);

            if (await HasWrongCategoryId(set))
            {
                return new Content { ID = Guid.Empty };
            }

            var categories = await _context.ContentCategories.Where(x => x.ContentId == content.ID).ToListAsync();
            _context.ContentCategories.RemoveRange(categories);
            set.UnionWith(categories.Select(x => x.CategoryId));

            _context.ContentCategories.AddRange(ContentModel.ModelCategoriesToContentCategories(content.ID, set));
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task<Content> DeleteCategory(Guid contentId, ICollection<int> categoryIds)
        {
            var content = new Content { ID = contentId };

            if (!(await CheckContentExist(contentId)))
            {
                return null;
            }

            var result = await DeleteCategory(content, categoryIds);

            if (result)
            {
                return content;
            }
            return null;
        }

        public async Task<int> CreateCategories(ICollection<Category> categories)
        {
            _context.Categories.AddRange(categories);
            return await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Category>> GetAllCategories()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        private async Task<bool> DeleteCategory(Content content, ICollection<int> categoryIds)
        {
            _context.ContentCategories.RemoveRange(
                _context.ContentCategories.Where(x => x.ContentId == content.ID && categoryIds.Contains(x.CategoryId)));

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<int> RemoveCategories(ICollection<int> categories)
        {
            _context.Categories.RemoveRange(_context.Categories.Where(x => categories.Contains(x.ID)));
            return await _context.SaveChangesAsync();
        }

        private async Task<bool> HasWrongCategoryId(ICollection<int> categories)
        {
            if (categories == null || categories.Count == 0) return false;
            return (await _context.Categories.Where(c => categories.Contains(c.ID)).CountAsync()) < categories.Count;
        }

        public async Task<bool> UpdateFileNames(Content content, Guid? file)
        {
            var e = _context.Contents.Attach(content);
            e.Entity.File = file ?? e.Entity.File;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Content> Get(Guid id)
        {
            return await _context.Contents.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Content> GetUserContent(Guid id, Guid userId)
        {
            var userContent = await _context.UserContents.FirstOrDefaultAsync(q => q.UserId == userId);

            if (userContent == null)
            {
                return null;
            }
            else
            {
                return await _context.Contents.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
            }
        }

        public async Task<ICollection<Content>> GetAllContent(int limit = 20, int offset = 0)
        {
            return await _context.Contents
                .ToPage(limit, offset)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<long> GetContentCount()
        {
            return await _context.Contents.LongCountAsync();
        }

        private async Task<Content> Delete(Content content)
        {
            if (content == null)
            {
                return null;
            }

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
            return content;
        }

        private async Task<Content> Update(Content content, ContentModel model)
        {
            if (content == null)
            {
                return null;
            }

            content.Title = (String.IsNullOrEmpty(model.Title)) ? content.Title : model.Title;
            content.Description = model.Description ?? content.Description;
            content.ValidityDate = model.ValidityDate ?? content.ValidityDate;

            Guid picId, fileId;

            if (Guid.TryParse(model.Image, out picId))
            {
                content.Image = picId;
            }
            if (Guid.TryParse(model.File, out fileId))
            {
                content.File = fileId;
            }

            await _context.SaveChangesAsync();
            return content;
        }

        private IQueryable<Content> GetContentQuery(Guid id)
        {
            return _context.Contents.Where(x => x.ID == id);
        }

        private async Task<bool> CheckContentExist(Guid contentId)
        {
            return await _context.Contents.AnyAsync(x => x.ID == contentId);
        }
        
        public async Task<long> FindCount(ContentFindParametersModel parameters)
        {
            return await Find(_context.Contents, parameters)
                .Distinct()
                .LongCountAsync();
        }

        private IQueryable<Content> Find(IQueryable<Content> query,
            ContentFindParametersModel parameters)
        {
            if (parameters.Categories != null && parameters.Categories.Count > 0)
            {
                query = from c in query
                        from cat in c.ContentCategories
                        where parameters.Categories.Contains(cat.CategoryId)
                        select c;
            }
            if (parameters.CreatorId.HasValue)
            {
                query = query.Where(x => x.CreatorId == parameters.CreatorId.Value);
            }
            if (parameters.MinValidity.HasValue)
            {
                query = query.Where(x => x.ValidityDate >= parameters.MinValidity.Value);
            }
            if (parameters.MaxValidity.HasValue)
            {
                query = query.Where(x => x.ValidityDate < parameters.MaxValidity.Value);
            }
            if (!String.IsNullOrEmpty(parameters.Title))
            {
                query = query.Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{parameters.Title.ToLower()}%"));
            }
            if (!String.IsNullOrEmpty(parameters.Description))
            {
                query = query.Where(x => EF.Functions.Like(x.Description.ToLower(), $"%{parameters.Description.ToLower()}%"));
            }

            return query.AsNoTracking();
        }

        public async Task<ICollection<Content>> Find(ContentFindParametersModel parameters, int limit = 20, int offset = 0)
        {
            var contentQuery = _context.Contents.Include(x => x.ContentCategories).ThenInclude(x => x.Category);

            return await Find(contentQuery, parameters)
                .Distinct()
                .ToPage(limit, offset)
                .ToListAsync();
        }
    }
}