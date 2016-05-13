using System;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using Newtonsoft.Json;
using ZCKT.AppServices;
using ZCKT.DTOs;
using ZCKT.ValueObjects;

namespace ZCKT.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountAppService accountAppService;

        public HomeController(AccountAppService accountAppService)
        {
            this.accountAppService = accountAppService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login/{username}")]
        public ActionResult Login(string username)
        {
            MembershipContext membership = accountAppService.Login(new LoginInputDto
            {
                Username = username
            });

            if (membership.IsAuthenticated)
            {
                //创建认证Token
                var identity = new GenericIdentity(membership.UserName);
                IPrincipal principal = new GenericPrincipal(identity, membership.Roles);
                Thread.CurrentPrincipal = principal;
                System.Web.HttpContext.Current.User = principal;

                //set cookies
                var data = new
                {
                    username = username,
                    authdata = Convert.ToBase64String(Encoding.Default.GetBytes($"{username}:")),
                    islogged = true
                };
                var jsonData = JsonConvert.SerializeObject(data);
                Response.Cookies.Add(new System.Web.HttpCookie("userData", jsonData));

                return RedirectToAction("Index");
            }

            return new HttpUnauthorizedResult($"user [{username}] authenticate failure!");
        }
    }
}