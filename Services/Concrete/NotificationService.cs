using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using dytsenayasar.Models.NotificationService;
using dytsenayasar.Services.Abstract;
using dytsenayasar.Util;
using System;
using dytsenayasar.Util.BackgroundQueueWorker;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace dytsenayasar.Services.Concrete
{
    public class NotificationService : INotificationService
    {
        private const int NOTIFICATION_USER_LIMIT = 100;

        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private readonly IBackgroundWorkHelper _backgroundHelper;

        public NotificationService(HttpClient httpClient, ILogger<NotificationService> logger,
            IUserService userService, IBackgroundWorkHelper backgroundHelper)
        {
            httpClient.BaseAddress = new System.Uri("https://fcm.googleapis.com");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                "key=AAAAImY8Rgk:APA91bGQPsbZm4OJU2TBUpxwh90dtOowFXCWj0fDkJcHnRtG5ZTqFou40ExNakyp5hH6v6UC_Xmwq30FbanJ5oAIsrwWZLxxxAO1OgY8O29cLVG1ExUPXK-OBcg8pASnA3faUDmWTJmz");

            _client = httpClient;
            _logger = logger;
            _backgroundHelper = backgroundHelper;
        }

        public async Task<int> Send<T>(NotificationMessageModel<T> message)
        {
            return await Send(ConvertNotificationToJson(message));
        }

        public void SendData<T>(NotificationDataType notificationDataType, ICollection<Guid> userIds, T data = default(T))
        {
            var offset = 0;
            ICollection<Guid> uids = null;

            do
            {
                uids = userIds.Skip(offset * NOTIFICATION_USER_LIMIT).Take(NOTIFICATION_USER_LIMIT).ToList();
                offset++;

                _backgroundHelper.Queue(async (scope, cancelToken) =>
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                    var notfService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    var clientIds = await userService.GetClientIds(uids);
                    _ = notfService.Send(new Models.NotificationService.NotificationMessageModel<T>
                    {
                        ClientIds = clientIds,
                        Data = new NotificationData<T>
                        {
                            Type = notificationDataType,
                            Value = data
                        }
                    });

                });

            } while (uids.Count >= NOTIFICATION_USER_LIMIT);
        }

        private async Task<int> Send(string postData)
        {
            if (string.IsNullOrWhiteSpace(postData)) return 0;

            var response = await _client.PostAsJsonAsync("/fcm/send", postData);
            var responseMessage = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Notification send failed: {0}", responseMessage);
            }

            var model = JsonConvert.DeserializeObject<ResponseModel>(responseMessage);
            return model.Success;
        }

        private string ConvertNotificationToJson<T>(NotificationMessageModel<T> message)
        {
            if (message.ClientIds.Count == 0)
            {
                return null;
            }

            var notfBody = new
            {
                title = message.Title,
                body = message.Message,
                click_action = message.Action
            };
            if (message.Title == null) notfBody = null;

            var postData = new
            {
                notification = notfBody,
                data = message.Data,
                registration_ids = message.ClientIds
            };

            return JsonConvert.SerializeObject(postData);
        }
    }

    class ResponseModel
    {
        public int Success { get; set; }
        public int Failure { get; set; }
    }
}