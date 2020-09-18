using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dyt_ecommerce.DataAccess.Entities;
using dyt_ecommerce.Services.Abstract;
using dyt_ecommerce.Util;
using dytsenayasar.Context;
using Microsoft.EntityFrameworkCore;

namespace dyt_ecommerce.Services.Concrete
{
    public class UserRequestService : IUserRequestService
    {
        private readonly ApplicationDbContext _context;

        public UserRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserRequest> CreateRequest(Guid userId, DateTime validity, UserRequestType type)
        {
            var result = await CreateRequest(new[] { userId }, validity, type);
            return result.FirstOrDefault();
        }

        public async Task<ICollection<UserRequest>> CreateRequest(IEnumerable<Guid> userIds, DateTime validity, UserRequestType type)
        {
            var requests = new List<UserRequest>();

            foreach (var id in userIds)
            {
                requests.Add(new UserRequest
                {
                    UserId = id,
                    ValidityDate = validity,
                    RequestType = type,
                    Token = new StringBuilder(id.ToString())
                        .Append(validity.ToLongTimeString())
                        .Append(DateTime.UtcNow.ToLongTimeString())
                        .ToString()
                        .HashToSha1()
                });
            }

            _context.UserRequests.RemoveRange(_context.UserRequests.Where(x => userIds.Contains(x.UserId) && x.RequestType == type));
            _context.UserRequests.AddRange(requests);
            await _context.SaveChangesAsync();
            return requests;
        }

        public async Task<bool> CheckRequestExists(Guid requestId, string token)
        {
            return await _context.UserRequests.AnyAsync(x => x.ID == requestId && x.ValidityDate > DateTime.UtcNow && x.Token == token);
        }

        public async Task<UserRequest> GetRequest(Guid requestId, string token, UserRequestType type)
        {
            return await _context.UserRequests
                .Where(x => x.ID == requestId && x.RequestType == type && x.Token == token)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteRequest(UserRequest request)
        {
            _context.UserRequests.Attach(request);
            _context.UserRequests.Remove(request);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}