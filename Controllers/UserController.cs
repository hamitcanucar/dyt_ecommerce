using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Models;
using dytsenayasar.Models.ControllerModels;
using dytsenayasar.Models.ControllerModels.UserControllerModels;
using dytsenayasar.Models.Settings;
using dytsenayasar.Services.Abstract;
using dytsenayasar.Util;
using dytsenayasar.Util.RazorViewRenderer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dytsenayasar.Controllers
{
    [Route("user")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class UserController : AController<UserController>
    {
        private const string LOGO_NAME = "slib-logo.png";
        private const string EMAIL_TEMPLATE = "Views/Emails/EmailWithConfirmButton.cshtml";

        private readonly IUserService _userService;
        private readonly IUserRequestService _userRequestService;
        private readonly IEmailService _emailService;
        private readonly AppSettings _appSettings;
        private readonly IRazorViewRenderer _viewRenderer;
        private readonly IStringLocalizer _localizer;

        public UserController(ILogger<UserController> logger, IUserService userService,
            IUserRequestService userRequestService,
            IEmailService emailService, IRazorViewRenderer viewRenderer,
            IStringLocalizer localizer, IOptions<AppSettings> appSettings) : base(logger)
        {
            _userService = userService;
            _userRequestService = userRequestService;
            _emailService = emailService;
            _appSettings = appSettings.Value;
            _viewRenderer = viewRenderer;
            _localizer = localizer;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("forgot")]
        public async Task<GenericResponse<string>> ForgotPassword([FromQuery] string q)
        {
            var user = await _userService.Get(q);

            if (user == null)
            {
                return new GenericResponse<string>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND
                };
            }

            var request = await _userRequestService.CreateRequest(user.ID,
                DateTime.UtcNow.AddDays(1), UserRequestType.PasswordReset);

            var baseUrl = _appSettings.Address;
            var emailModel = new Views.Emails.EmailWithConfirmButtonModel
            {
                LogoUrl = $"{baseUrl}/{LOGO_NAME}",
                Title = _localizer["reset_pass_email_title"],
                Preview = _localizer["reset_pass_email_desc"],
                Description = _localizer["reset_pass_email_desc"],
                UrlDescription = _localizer["reset_pass_email_url_desc"],
                Url = string.Format(PagesController.PW_RESET_URL, baseUrl, request.ID, request.Token),
                ButtonText = _localizer["reset_pass_email_btn_txt"]
            };

            var mailStr = await _viewRenderer.RenderViewToString(EMAIL_TEMPLATE, emailModel);
            _ = _emailService.Send(new Models.EmailManager.EmailMessageModel
            {
                Subject = emailModel.Title,
                Content = mailStr,
                ToAdresses = new[] { new Models.EmailManager.EmailAddressModel { Name = user.FirstName, Address = user.Email } }
            });

            return new GenericResponse<string> { Success = true };
        }

        [HttpPost]
        [Route("register")]
        public async Task<GenericResponse<UserModel>> Register([FromBody] RegisterRequestModel model)
        {
            var user = model.ToModel();
            var result = await _userService.Register(user);

            if (result == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }

            return new GenericResponse<UserModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpPost]
        [Route("login")]
        public async Task<GenericResponse<UserModel>> Login([FromBody] LoginRequestModel model)
        {
            var result = await _userService.Login(model.PIDorEmail, model.Password);

            if (result == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.LOGIN_WRONG_CRIDENTIALS),
                    Message = ErrorMessages.LOGIN_WRONG_CRIDENTIALS
                };
            }
            else if (result.Token == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.LOGIN_DEACTIVE_USER),
                    Message = ErrorMessages.LOGIN_DEACTIVE_USER,
                    Data = result
                };
            }

            return new GenericResponse<UserModel>
            {
                Success = true,
                Data = result
            };
        }

        [HttpPost]
        [Route("form")]
        [Authorize]
        public async Task<GenericResponse<UserFormModel>> UserForm([FromBody] UserFormRequestModel model)
        {
            var userId = GetUserIdFromToken();
            var userForm = model.ToModel();
            var result = await _userService.UserForm(userForm, userId);

            if (result == null)
            {
                return new GenericResponse<UserFormModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }

            return new GenericResponse<UserFormModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<UserModel> GetUser(Guid id)
        {
            var user = await _userService.Get(id);

            if (user == null)
            {
                return null;
            }
            else
            {
                return user.ToModel();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserModel>> GetUserList([FromQuery(Name = "q")] string ids)
        {
            var guids = ids.SplitToGuid().ToList();
            if (guids.Count == 0)
                return null;

            var result = await _userService.Get(guids);
            return result.Select(u => u.ToModel());
        }

        [HttpGet]
        [Route("find")]
        [Authorize]
        public async Task<ICollection<UserModel>> Find([FromQuery] int limit = 20, [FromQuery] int offset = 0,
            [FromQuery] string searchValue = null,
            [FromQuery] string phone = null,
            [FromQuery] int? age = null,
            [FromQuery] GenderType? gender = null,
            [FromQuery] UserType? userType = null,
            [FromQuery] bool getCount = false)
        {
            var now = DateTime.UtcNow;
            var searchParameters = new UserFindParametersModel
            {
                SearchValue = searchValue,
                Phone = phone,
                Gender = gender,
                UserType = userType
            };

            if (age.HasValue)
            {
                searchParameters.BirthDate = now.AddYears(-age.Value);
            }

            if (getCount)
            {
                var count = await _userService.FindCount(searchParameters);
                Response.AddCount(count);
            }

            var result = await _userService.Find(searchParameters, limit, offset);

            return result.Select(x => x.ToModel()).ToList();
        }

        [HttpPatch]
        [Authorize]
        public async Task<GenericResponse<UserModel>> UpdateUser([FromBody] UpdateUserRequestModel model)
        {
            var result = await _userService.Update(GetUserIdFromToken(), model.ToModel(), model.Password);

            if (result == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }
            else if (Guid.Equals(Guid.Empty, result.ID))
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND
                };
            }

            return new GenericResponse<UserModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpPatch]
        [Route("password")]
        [Authorize]
        public async Task<GenericResponse<string>> UpdatePassword([FromBody] UpdatePasswordRequestModel model)
        {
            var result = await _userService.UpdatePassword(GetUserIdFromToken(), model.OldPassword, model.NewPassword);

            if (result == null)
                return new GenericResponse<string>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND
                };
            if (result.ID == Guid.Empty)
                return new GenericResponse<string>
                {
                    Code = nameof(ErrorMessages.USER_UPDATE_WRONG_OLDPASS),
                    Message = ErrorMessages.USER_UPDATE_WRONG_OLDPASS
                };

            return new GenericResponse<string> { Success = true };
        }

        [HttpPost]
        [Route("client")]
        [Authorize]
        public async Task<GenericResponse<string>> SaveClientId([FromBody] SaveClientIdRequestModel model)
        {
            var result = await _userService.SaveClientId(GetUserIdFromToken(), model.ClientId);

            return new GenericResponse<string> { Success = result };
        }

    }
}