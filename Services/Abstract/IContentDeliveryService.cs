using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dyt_ecommerce.DataAccess.Entities;
using dyt_ecommerce.Models;
using dytsenayasar.DataAccess.Entities;

namespace dyt_ecommerce.Services.Abstract
{
    public interface IContentDeliveryService
    {
        Task<ICollection<UserContent>> GetAllContents(Guid userId, string search = null);
        Task<ICollection<UserContent>> GetAllContents(Guid userId, Guid requestingUserId, string search = null);
        Task<ICollection<Guid>> GetAllContentOwnersId(Guid contentId);
        Task<bool> CheckContentTaken(Guid contentId, Guid userId);
        Task<bool> CheckContentFileAvailableForUser(Guid fileId, Guid requestingUserId);
        Task<ICollection<Guid>> CheckAvailableContentFiles(Guid userId, ICollection<Guid> contentIds);
        
        //Returns userIds who contents updated
        Task<(long, ICollection<Guid>, Dictionary<Guid, long>)> BulkDeliverContent(ICollection<Guid> contentIds, DateTime validity, DeliveryType deliveryType,
            ICollection<Guid> userIds = null, ICollection<UserType> userTypes = null, Guid? corpId = null, Guid? schoolId = null, 
            Guid? classId = null, Guid? requestingUserId = null);
        Task<ICollection<Guid>> DeleteDelivers(ICollection<Guid> contentIds, DeliveryType deliveryType,
            ICollection<Guid> userIds = null, ICollection<UserType> userTypes = null, Guid? corpId = null, 
            Guid? schoolId = null, Guid? classId = null, Guid? requestingUserId = null);
        Task<ICollection<User>> FindContentUsers(Guid contentId, UserFindParametersModel findParams, 
            DeliveryType? deliveryType = null, int limit = 20, int offset = 0);
        Task<long> FindContentUsersCount(Guid contentId, UserFindParametersModel findParams, 
            DeliveryType? deliveryType = null);
        
        //Returns common contents in specified users
        Task<ICollection<Content>> GetCommonContents(DeliveryType? deliveryType = null, ICollection<Guid> userIds = null, 
            ICollection<UserType> userTypes = null, Guid? corpId = null, Guid? schoolId = null, Guid? classId = null, Guid? requestingUserId = null,
            int limit = 20, int offset = 0);
        Task<long> GetCommonContentsCount(DeliveryType? deliveryType = null, ICollection<Guid> userIds = null,
            ICollection<UserType> userTypes = null, Guid? corpId = null, Guid? schoolId = null, Guid? classId = null, Guid? requestingUserId = null);
    }
}