using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZCKT.AppServices;
using ZCKT.DTOs;
using ZCKT.ValueObjects;
using ZCKT.WebApi;

namespace ZCKT.Web.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        private readonly AccountAppService accountAppService;

        public AccountController(AccountAppService accountAppService)
        {
            this.accountAppService = accountAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("authenticate/{username}")]
        public HttpResponseMessage Login(HttpRequestMessage request, string username)
        {
            return CreateHttpResponse(request, () =>
            {
                MembershipContext loginResult = this.accountAppService.Login(
                    new LoginInputDto
                    {
                        Username = username.Trim()
                    });
                return request.CreateResponse(HttpStatusCode.OK, new { success = loginResult.IsAuthenticated });
            });
        }
    }
}