using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Services.Abstract
{
    public interface IUserRequestService
    {
         Task<UserRequest> CreateRequest(Guid userId, DateTime validity, UserRequestType type);
         Task<ICollection<UserRequest>> CreateRequest(IEnumerable<Guid> userIds, DateTime validity, UserRequestType type);
         Task<bool> CheckRequestExists(Guid requestId, string token);
         Task<UserRequest> GetRequest(Guid requestId, string token, UserRequestType type);
         Task<bool> DeleteRequest(UserRequest request);
    }
}