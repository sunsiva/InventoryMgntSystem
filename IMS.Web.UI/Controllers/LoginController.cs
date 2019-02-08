using IMS.Business.Common;
using IMS.Business.Provider;
using IMS.Business.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;
using IMS.Common.Data.Interfaces;
using IMS.Business.Presenters;
using Compaid.Common.Ioc.TinyIoc;
using IMS.Web.Provider;
using IMS.Web.UI.Models;

namespace IMS.Web.UI.Controllers
{
    
    [RoutePrefix("login")]
    public class LoginController : Controller
    {
        private ILoginPresenter _loginPresenter;
        private ICookieAuthentationProvider _cookieProvider;
        private IForgotPasswordPresenter _forgotPasswordPresenter;
        private IResetPasswordPresenter _resetPasswordPresenter;
        private IConfigurationProvider _configProvider;
        private IUserIdentityProvider userProvider;       

        public LoginController(ILoginPresenter loginPresenter,
            ICookieAuthentationProvider cookieProvider,
            IForgotPasswordPresenter forgotPasswordPresenter,
            IResetPasswordPresenter resetPasswordPresenter,
            IConfigurationProvider configProvider,
            IUserIdentityProvider user)
        {
            _loginPresenter = loginPresenter;
            _cookieProvider = cookieProvider;
            _forgotPasswordPresenter = forgotPasswordPresenter;
            _resetPasswordPresenter = resetPasswordPresenter;
            _configProvider = configProvider;
            userProvider = user;
        }

        // GET: Login
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(string returnUrl)
        {
            var model = new LoginModel();
            model.ReturnUrl = returnUrl;
            if (_loginPresenter != null)
            {

                if (_loginPresenter.IsUserLoggedIn())
                {
                    var userProvider = TinyIocContainer.Current.Resolve<IUserIdentityProvider>();
                    return RedirectToAction("MySummary", "Summeries");
                }
            }

            return View(model);
        }
                
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            var loginModel = model;

            if (!String.IsNullOrEmpty(model.LoginUsername) && !String.IsNullOrEmpty(model.LoginPassword))
            {                
                loginModel = _loginPresenter.AttemptLogin(model.LoginUsername, model.LoginPassword);

                if (loginModel.LoginSuccess)
                {
                    HttpCookie cookie = _cookieProvider.CreateCookie(loginModel.UserPrincipalData.Name, loginModel.UserPrincipalData.UserKey);
                    Response.Cookies.Add(cookie);

                    if (loginModel.NeedsPasswordChange)
                        return RedirectToAction("ChangePassword", new { userName = model.LoginUsername });

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return RedirectToAction("MySummary", "Summeries");
                    else
                        return Redirect(model.ReturnUrl);
                }
            }

            //var model = new LoginModel();
            model.FailedLogin = true;
            model.Message = loginModel.Message;

            return View("Index", model);
        }

        public ActionResult AccessDenied()
        {
            ViewBag.ErrorMessage = TempData.ContainsKey("AccessDeniedErrorMessage") ?
                TempData["AccessDeniedErrorMessage"] :
                IMSResourceManager.GetApplicationString(AppConstants.ErrorResourceName.ACCESSDENIED);
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Logout()
        {
            _loginPresenter.Logout();

            ICookieAuthentationProvider cookieProvider = TinyIocContainer.Current.Resolve<ICookieAuthentationProvider>();

            cookieProvider.DeleteCookie(HttpContext.Request, HttpContext.Response);

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        [IMSAuthentication(Roles = "Admin")]
        public ActionResult ChangePassword(string userName)
        {
            var model = new ChangePasswordViewModel();
            model.Username = userName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [IMSAuthentication(Roles = "Admin")]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _loginPresenter.ChangePassword(model);

                if (result.Failure)
                    ModelState.AddModelError("CustomError", result.ErrorMessage);
                else
                    return RedirectToAction("MySummary", "Summeries");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _forgotPasswordPresenter.ForgotPassword(model);
                if (result.Failure)
                    ModelState.AddModelError("CustomError", result.ErrorMessage);
                else
                    return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        [Route("resetpassword/{resetToken:guid}")]
        public ActionResult ResetPassword(Guid resetToken)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            if (!_resetPasswordPresenter.IsValidToken(resetToken))
            {
                ViewBag.ErrorMessage = "The password reset token is invalid or expired";
                return View("AccessDenied");
            }
            else
            {
                model.ResetPasswordToken = resetToken;
            }

            return View(model);
        }

        [HttpPost]
        [Route("resetpassword")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _resetPasswordPresenter.ResetPassword(model);
                if (result.Failure)
                    ModelState.AddModelError("CustomError", result.ErrorMessage);
                else
                    return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult _SessionTimeout()
        {
            var model = new SessionTimeoutModel
            {
                SessionTimeoutMinutes = _configProvider.GetConfigurationInt(AppConstants.ConfigSetting.SYSTEM_TIMEOUT),
                SessionTimeoutWarningMinutes = _configProvider.GetConfigurationInt(AppConstants.ConfigSetting.SYSTEM_TIMEOUT_WARNING)
            };
            return PartialView(model);
        }
    }
}