using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using dytsenayasar.DataAccess.Entities;
using dytsenayasar.Models.ControllerModels.PagesControllerModels;
using dytsenayasar.Services.Abstract;
using dytsenayasar.Util;

namespace dytsenayasar.Controllers
{
    [Route("pages")]
    [Controller]
    public class PagesController : Controller
    {
        public const string PW_RESET_URL = "{0}/pages/resetPassword?id={1}&token={2}";
        public const string ACTIVATE_ACCOUNT_URL = "{0}/pages/activateAccount?id={1}&token={2}";

        private readonly ILogger _logger;
        private readonly IStringLocalizer _localizer;
        private readonly IUserRequestService _userRequestService;
        private readonly IUserService _userService;

        public PagesController(ILogger<PagesController> logger, IStringLocalizer localizer,
            IUserRequestService userRequestService, IUserService userService)
        {
            _logger = logger;
            _localizer = localizer;
            _userRequestService = userRequestService;
            _userService = userService;
        }
        
        [Route("resetPassword")]
        [NonAction]
        public async Task<IActionResult> ResetPassword([FromQuery(Name = "id")] Guid requestId, [FromQuery(Name = "token")] string token)
        {
            var requestExists = await _userRequestService.CheckRequestExists(requestId, token);

            if (requestExists)
            {
                ViewData["show_form"] = true;
                ViewData["id"] = requestId;
                ViewData["token"] = token;
            }
            else
            {
                ViewData["show_form"] = false;
                ViewData["message"] = _localizer["reset_pass_req_nf"];
            }

            return View();
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery(Name = "id")] Guid requestId,
            [FromQuery(Name = "token")] string token, ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return await ResetPassword(requestId, token);
            }

            ViewData["show_form"] = false;
            var ur = await _userRequestService.GetRequest(requestId, token, UserRequestType.PasswordReset);

            if (ur != null)
            {

                if (ur.ValidityDate < DateTime.UtcNow)
                {
                    await _userRequestService.DeleteRequest(ur);
                    ViewData["message"] = _localizer["reset_pass_req_expired"];
                }
                else
                {
                    bool result = ur.User.Password == model.NewPassword.HashToSha256();
                    if (!result)
                    {
                        result = await _userService.UpdatePassword(ur.User, model.NewPassword);
                    }

                    if (result)
                    {
                        await _userRequestService.DeleteRequest(ur);
                        ViewData["message"] = _localizer["reset_pass_success"];
                    }
                    else
                    {
                        ViewData["message"] = _localizer["reset_pass_failure"];
                    }
                }
            }
            else
            {
                ViewData["message"] = _localizer["reset_pass_req_nf"];
            }

            return View();
        }

        [Route("activateAccount")]
        [HttpPost]
        public async Task<IActionResult> ActivateAccount([FromQuery(Name = "id")] Guid requestId, [FromQuery(Name = "token")] string token)
        {
            var ur = await _userRequestService.GetRequest(requestId, token, UserRequestType.ActivateAccount);

            if (ur != null)
            {
                if (ur.ValidityDate < DateTime.UtcNow)
                {
                    await _userRequestService.DeleteRequest(ur);
                    ViewData["message"] = _localizer["activate_account_req_expired"];
                }
                else
                {
                    var result = await _userService.ActivateUser(ur.User);
                    ViewData["success"] = result;

                    if(result)
                    {
                        await _userRequestService.DeleteRequest(ur);
                        ViewData["message"] = _localizer["activate_account_success"];
                    }
                    else
                    {
                        ViewData["message"] = _localizer["activate_account_failure"];
                    }
                }
            }
            else
            {
                ViewData["success"] = false;
                ViewData["message"] = _localizer["activate_account_req_nf"];
            }

            return View();
        }
    }
}