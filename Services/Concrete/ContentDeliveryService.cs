using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dyt_ecommerce.DataAccess;
using dyt_ecommerce.DataAccess.Entities;
using dyt_ecommerce.Models;
using dyt_ecommerce.Services.Abstract;
using dyt_ecommerce.Util;
using dytsenayasar.Context;
using dytsenayasar.DataAccess.Entities;

namespace dyt_ecommerce.Services.Concrete
{
    public class ContentDeliveryService : IContentDeliveryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IContentService _contentService;
        private readonly IUserService _userService;

        public ContentDeliveryService(ApplicationDbContext context, IContentService contentService, IUserService userService)
        {
            _context = context;
            _contentService = contentService;
            _userService = userService;
        }

        public async Task<ICollection<UserContent>> GetAllContents(Guid userId, string search = null)
        {
            var contentQuery = _context.Contents.AsQueryable();
            
            if(!string.IsNullOrWhiteSpace(search)){
                contentQuery = contentQuery.Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{search.ToLower()}%"));
            }

            var query = from c in contentQuery.Include(x => x.ContentCategories).ThenInclude(x => x.Category)
                        from uc in c.UserContents.OrderBy(x => x.DeliveryType).Where(x => x.UserId == userId && x.ValidityDate > DateTime.UtcNow).Take(1)
                        select new UserContent
                        {
                            ID = uc.ID,
                            DeliveryType = uc.DeliveryType,
                            ValidityDate = uc.ValidityDate,
                            ContentId = uc.ContentId,
                            Content = c
                        };

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Guid>> GetAllContentOwnersId(Guid contentId)
        {
            return await _context.UserContents.Where(x => x.ContentId == contentId)
                .Select(x => x.UserId).Distinct().ToListAsync();
        }

        public Task<bool> CheckContentTaken(Guid contentId, Guid userId)
        {
            return _context.UserContents.AnyAsync(x => x.ContentId == contentId && x.UserId == userId && x.ValidityDate > DateTime.UtcNow);
        }

        public async Task<bool> CheckContentFileAvailableForUser(Guid fileId, Guid requestingUserId)
        {
            var query = from uc in _context.UserContents
                        where uc.UserId == requestingUserId && uc.Content.File == fileId && uc.ValidityDate > DateTime.UtcNow
                        select uc.Content.File;

            return await query.AnyAsync(x => x == fileId);
        }

        public async Task<ICollection<Guid>> CheckAvailableContentFiles(Guid userId, ICollection<Guid> contentIds)
        {
            return await _context.UserContents
                .Where(x => x.UserId == userId && x.ValidityDate > DateTime.UtcNow && contentIds.Contains(x.ContentId))
                .Select(x => x.ContentId)
                .ToListAsync();
        }
    }
}