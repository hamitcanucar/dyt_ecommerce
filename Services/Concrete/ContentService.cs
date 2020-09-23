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

        public async Task<bool> UpdateFileNames(Content content, Guid? image, Guid? file)
        {
            var e = _context.Contents.Attach(content);
            e.Entity.Image = image ?? e.Entity.Image;
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
    }
}