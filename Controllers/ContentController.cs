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
        private readonly INotificationService _notificationService;
        private readonly IFileManager _fileManager;
        private readonly AppSettings _appSettings;

        public ContentController(ILogger<ContentController> logger,
            IContentService contentService,
            INotificationService notificationService, IFileManager fileManager, IOptions<AppSettings> appSettings) : base(logger)
        {
            _contentService = contentService;
            _notificationService = notificationService;
            _fileManager = fileManager;
            _appSettings = appSettings.Value;
        }
    }
}