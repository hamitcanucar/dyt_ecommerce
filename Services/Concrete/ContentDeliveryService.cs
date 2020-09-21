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

        public async Task<ICollection<Content>> GetCommonContents(DeliveryType? deliveryType = null, ICollection<Guid> userIds = null,
            ICollection<UserType> userTypes = null, Guid? corpId = null, Guid? schoolId = null, Guid? classId = null, Guid? requestingUserId = null,
            int limit = 20, int offset = 0)
        {
            return await GetCommonContentsQuery(deliveryType, userIds, userTypes, corpId, schoolId, classId, requestingUserId)
                .ToPage(limit, offset)
                .ToListAsync();
        }

        public Task<long> GetCommonContentsCount(DeliveryType? deliveryType = null, ICollection<Guid> userIds = null,
            ICollection<UserType> userTypes = null, Guid? corpId = null, Guid? schoolId = null, Guid? classId = null, Guid? requestingUserId = null)
        {
            return GetCommonContentsQuery(deliveryType, userIds, userTypes, corpId, schoolId, classId, requestingUserId)
                .LongCountAsync();
        }

        /*
            - User Not Found                    -> return (0, null, null)
            - There are no available content    -> return (usersCount, null, null)
            - Success                           -> return (usersCount, UserIds, Dictionary<contentId, deliverCount>)
        */
        
        public async Task<ICollection<Guid>> DeleteDelivers(ICollection<Guid> contentIds, DeliveryType deliveryType,
            ICollection<Guid> userIds = null, ICollection<UserType> userTypes = null, Guid? corpId = null, Guid? schoolId = null, Guid? classId = null,
            Guid? requestingUserId = null)
        {
            var users = from m in CreateUserMembershipsQuery(userIds, userTypes, corpId, schoolId, classId, requestingUserId)
                        select m.UserId;

            var contentsForDelete = _context.UserContents
                .Where(uc => uc.DeliveryType == deliveryType && contentIds.Contains(uc.ContentId) && users.Contains(uc.UserId));

            // Delete delivered contents to users who has no memberships (Corp, School, Class)
            if (userIds != null)
            {
                var deliversToNoMemberships = _context.UserContents
                    .Where(uc => uc.DeliveryType == deliveryType && contentIds.Contains(uc.ContentId) && userIds.Contains(uc.UserId)
                        && (userTypes == null || userTypes.Contains(uc.User.UserType)));

                contentsForDelete = contentsForDelete.Union(deliversToNoMemberships);
            }

            var result = await contentsForDelete.ToListAsync();

            _context.UserContents.RemoveRange(result);
            await _context.SaveChangesAsync();

            return result.Select(x => x.UserId).Distinct().ToList();
        }

        public async Task<ICollection<User>> FindContentUsers(Guid contentId, UserFindParametersModel findParams,
            DeliveryType? deliveryType = null, int limit = 20, int offset = 0)
        {
            return await FindContentUsers(contentId, findParams, deliveryType)
                .ToPage(limit, offset)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<long> FindContentUsersCount(Guid contentId, UserFindParametersModel findParams,
            DeliveryType? deliveryType = null)
        {
            return FindContentUsers(contentId, findParams, deliveryType)
                .LongCountAsync();
        }

        private IQueryable<User> FindContentUsers(Guid contentId, UserFindParametersModel findParams,
            DeliveryType? deliveryType = null)
        {
            var userFindQuery = _userService.CreateUserFindQuery(findParams);
            var contentUsersQuery = from uc in _context.UserContents
                                    where uc.ContentId == contentId && uc.ValidityDate > DateTime.UtcNow
                                    select uc;

            if (deliveryType.HasValue)
            {
                contentUsersQuery = contentUsersQuery.Where(x => x.DeliveryType == deliveryType);
            }

            return userFindQuery
                .Where(x => contentUsersQuery.Select(y => y.UserId).Contains(x.ID));
        }
    }
}