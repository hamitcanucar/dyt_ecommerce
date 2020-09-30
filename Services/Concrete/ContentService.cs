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

        public async Task<Content> Create(ContentModel model)
        {
            var content = model.ToEntity();

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

        public async Task<Content> Get(Guid id)
        {
            return await _context.Contents.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Content> GetUserContent(Guid id, Guid userId)
        {
            var userContent = await _context.Contents.FirstOrDefaultAsync(q => q.User.ID == userId);

            if (userContent == null)
            {
                return null;
            }
            else
            {
                return await _context.Contents.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
            }
        }

        public async Task<ICollection<Guid>> GetAllContentOwnerId(Guid contentId)
        {
            return await _context.Contents.Where(x => x.ID == contentId)
                .Select(x => x.User.ID).Distinct().ToListAsync();
        }

        public async Task<bool> CheckContentFileAvailableForUser(Guid fileId, Guid requestingUserId)
        {
            var query = from uc in _context.Contents
                        where uc.User.ID == requestingUserId && uc.File == fileId && uc.UploadDate > DateTime.UtcNow
                        select uc.File;

            return await query.AnyAsync(x => x == fileId);
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
            content.UploadDate = model.ValidityDate ?? content.UploadDate;

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
            if (parameters.MinValidity.HasValue)
            {
                query = query.Where(x => x.UploadDate >= parameters.MinValidity.Value);
            }
            if (parameters.MaxValidity.HasValue)
            {
                query = query.Where(x => x.UploadDate < parameters.MaxValidity.Value);
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
    }
}