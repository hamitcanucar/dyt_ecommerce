using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dytsenayasar.Models.NotificationService;

namespace dytsenayasar.Services.Abstract
{
    public interface INotificationService
    {
        Task<int> Send<T>(NotificationMessageModel<T> message);
        void SendData<T>(NotificationDataType notificationDataType, ICollection<Guid> userIds, T data = default(T));
    }
}