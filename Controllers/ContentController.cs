using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Models;
using dytsenayasar.Models.ControllerModels;
using dytsenayasar.Models.ControllerModels.ContentControllerModels;
using dytsenayasar.Models.Settings;
using dytsenayasar.Services.Abstract;
using dytsenayasar.Util;

namespace dytsenayasar.Controllers
{
    [Route("content")]
    [ApiController]
    public class ContentController : AController<ContentController>
    {
        private readonly IContentService _contentService;
        private readonly IContentDeliveryService _contentDeliveryService;
        private readonly INotificationService _notificationService;
        private readonly IFileManager _fileManager;
        private readonly AppSettings _appSettings;

        public ContentController(ILogger<ContentController> logger,
            IContentService contentService, IContentDeliveryService contentDeliveryService,
            INotificationService notificationService, IFileManager fileManager, IOptions<AppSettings> appSettings) : base(logger)
        {
            _contentService = contentService;
            _contentDeliveryService = contentDeliveryService;
            _notificationService = notificationService;
            _fileManager = fileManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<GenericResponse<ContentModel>> CreateContent([FromBody] ContentRequestModel model)
        {

            var result = await _contentService.Create(model.ToModel(), GetUserIdFromToken());

            if (result == null)
            {
                return new GenericResponse<ContentModel>
                {
                    Code = nameof(ErrorMessages.CONTENT_HAS_WRONG_CATEGORY_ID),
                    Message = ErrorMessages.CONTENT_HAS_WRONG_CATEGORY_ID
                };
            }

            return new GenericResponse<ContentModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<GenericResponse<ContentModel>> UpdateContent(Guid id, [FromBody] ContentUpdateRequestModel model)
        {
            Content result;

            result = await _contentService.Update(id, model.ToModel());

            if (result == null)
            {
                return new GenericResponse<ContentModel>
                {
                    Code = nameof(ErrorMessages.CONTENT_NOT_FOUND),
                    Message = ErrorMessages.CONTENT_NOT_FOUND
                };
            }

            if (result.ID == Guid.Empty)
            {
                return new GenericResponse<ContentModel>
                {
                    Code = nameof(ErrorMessages.CONTENT_HAS_WRONG_CATEGORY_ID),
                    Message = ErrorMessages.CONTENT_HAS_WRONG_CATEGORY_ID
                };
            }

            return new GenericResponse<ContentModel>
            {
                Success = true,
                Data = ConvertContentModel(result)
            };
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<GenericResponse<ContentModel>> DeleteContent(Guid id)
        {
            var owners = await _contentDeliveryService.GetAllContentOwnersId(id);
            var result = await _contentService.Delete(id);

            if (result == null)
            {
                return new GenericResponse<ContentModel>
                {
                    Code = nameof(ErrorMessages.CONTENT_NOT_FOUND),
                    Message = ErrorMessages.CONTENT_NOT_FOUND
                };
            }

            if (result.Image.HasValue)
            {
                _ = _fileManager.DeleteImage(result.Image.ToString());
                result.Image = null;
            }
            if (result.File.HasValue)
            {
                _ = _fileManager.DeleteFile(result.File.ToString());
                result.File = null;
            }

            _notificationService
                .SendData<string>(Models.NotificationService.NotificationDataType.UserContentsUpdated, owners);

            return new GenericResponse<ContentModel>
            {
                Success = true,
                Data = ConvertContentModel(result)
            };
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<ContentModel> GetContent(Guid id)
        {
            Content content;

            if (User.IsInRole(Role.ADMIN))
            {
                content = await _contentService.Get(id);
            }
            else
            {
                content = await _contentService.GetUserContent(id, GetUserIdFromToken());
            }

            if (content == null)
            {
                return null;
            }

            var result = ConvertContentModel(content);

            return result;
        }

        [HttpPut]
        [Route("{id}/categories")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<GenericResponse<ContentModel>> AddContentCategories(Guid id, [FromBody] ICollection<int> categoryIds)
        {
            Content result;

            result = await _contentService.AddCategory(id, categoryIds);

            if (result == null)
            {
                return new GenericResponse<ContentModel>
                {
                    Code = nameof(ErrorMessages.CONTENT_NOT_FOUND),
                    Message = ErrorMessages.CONTENT_NOT_FOUND
                };
            }

            if (result.ID == Guid.Empty)
            {
                return new GenericResponse<ContentModel>
                {
                    Code = nameof(ErrorMessages.CONTENT_HAS_WRONG_CATEGORY_ID),
                    Message = ErrorMessages.CONTENT_HAS_WRONG_CATEGORY_ID
                };
            }

            return new GenericResponse<ContentModel>
            {
                Success = true,
                Data = ConvertContentModel(result)
            };
        }

        [HttpPost]
        [Route("{id}/categories/delete")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<GenericResponse<string>> DeleteContentCategories(Guid id, [FromBody] ICollection<int> categoryIds)
        {
            Content result;

            result = await _contentService.DeleteCategory(id, categoryIds);

            if (result == null)
            {
                return new GenericResponse<string>
                {
                    Code = nameof(ErrorMessages.NOTHING_CHANGED),
                    Message = ErrorMessages.NOTHING_CHANGED
                };
            }

            return new GenericResponse<string>
            {
                Success = true
            };
        }

        [HttpGet]
        [Authorize]
        [Route("categories")]
        public async Task<IDictionary<int, string>> GetAllCategories()
        {
            return ContentModel.ConvertContentCategoryToDictionary(await _contentService.GetAllCategories());
        }

        [HttpPost]
        [Route("categories")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<GenericResponse<int>> CreateCategories([FromBody] ICollection<CategoryRequestModel> models)
        {
            if (models == null || models.Count == 0) return new GenericResponse<int>();
            var result = await _contentService.CreateCategories(models.Select(x => x.ToModel()).ToList());
            return new GenericResponse<int>
            {
                Success = result > 0,
                Data = result
            };
        }

        [HttpPost]
        [Route("categories/delete")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<GenericResponse<int>> DeleteCategories([FromBody] ICollection<int> ids)
        {
            if (ids == null || ids.Count == 0) return new GenericResponse<int>();
            var result = await _contentService.RemoveCategories(ids);
            return new GenericResponse<int>
            {
                Success = result > 0,
                Data = result
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<ICollection<ContentModel>> GetAllContents([FromQuery] int limit = 20, [FromQuery] int offset = 0,
            [FromQuery] bool getCount = false)
        {
            ICollection<ContentModel> result;
            long count = 0;
            var userId = GetUserIdFromToken();

            result = ConvertContentModels(await _contentService.GetAllContent(limit, offset), true);
            if (getCount) count = await _contentService.GetContentCount();


            Response.AddCount(count);
            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("find")]
        public async Task<ICollection<ContentModel>> Find([FromQuery] int limit = 20, [FromQuery] int offset = 0,
            [FromQuery] bool getCount = false,
            [FromQuery] string categories = null,
            [FromQuery] Guid? creatorId = null,
            [FromQuery] DateTime? minValidity = null,
            [FromQuery] DateTime? maxValidity = null,
            // [FromQuery] string contentTypes = null,
            [FromQuery] string title = null,
            [FromQuery] string description = null)
        {
            // var contentTypeList = contentTypes.SplitToEnum<ContentType>();
            var categoryIds = categories.SplitToInt();

            ICollection<ContentModel> result;
            long count = 0;
            var parameters = new ContentFindParametersModel
            {
                Categories = categoryIds.ToList(),
                CreatorId = creatorId,
                MinValidity = minValidity,
                MaxValidity = maxValidity,
                // ContentType = contentTypeList.ToList(),
                Title = title,
                Description = description
            };

            result = ConvertContentModels(await _contentService.Find(parameters, limit, offset), true);
            if (getCount) count = await _contentService.FindCount(parameters);

            Response.AddCount(count);
            return result;
        }

        private ICollection<ContentModel> ConvertContentModels(ICollection<(Content, bool)> contents)
        {
            return contents.Select(x => new ContentModel(x.Item1, x.Item2, _appSettings.Address)).ToList();
        }

        private ICollection<ContentModel> ConvertContentModels(ICollection<Content> contents, bool taken = false)
        {
            return contents.Select(x => new ContentModel(x, taken, _appSettings.Address)).ToList();
        }

        private ContentModel ConvertContentModel(Content content)
        {
            return content.ToModel(_appSettings.Address);
        }
    }
}