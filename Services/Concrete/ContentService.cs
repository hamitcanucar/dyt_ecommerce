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

        public async Task<Content> Delete(Guid id)
        {
            var content = await _context.Contents.FindAsync(id);
            return await Delete(content);
        }

        public async Task<Content> Get(Guid id)
        {
            return await _context.Contents.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Content> GetUserContent(Guid id, Guid userId)
        {
            var userContent = await _context.Contents.FirstOrDefaultAsync(q => q.UserId == userId);

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
                .Select(x => x.UserId).Distinct().ToListAsync();
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

        private IQueryable<Content> GetContentQuery(Guid id)
        {
            return _context.Contents.Where(x => x.ID == id);
        }

        private async Task<bool> CheckContentExist(Guid contentId)
        {
            return await _context.Contents.AnyAsync(x => x.ID == contentId);
        }

    }
}