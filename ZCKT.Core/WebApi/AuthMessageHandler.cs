using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Dependencies;
using ZCKT.AppServices;
using ZCKT.DTOs;
using ZCKT.ValueObjects;

namespace ZCKT.WebApi
{
    public class AuthMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> authHeaderValues = null;
            request.Headers.TryGetValues("Authorization", out authHeaderValues);
            if (authHeaderValues == null)
                return base.SendAsync(request, cancellationToken);  //next IMessageHandler

            var tokens = authHeaderValues.FirstOrDefault(); //Basic amVyZW15eWFuZzoxMjM0NTY=
            if (tokens != null)
                tokens = tokens.Replace("Basic", "").Trim();
            if (!string.IsNullOrEmpty(tokens))
            {
                byte[] data = Convert.FromBase64String(tokens);
                string decodeString = Encoding.UTF8.GetString(data);
                string[] tokensValues = decodeString.Split(':');
                string userUame = tokensValues[0];
                string hashPassword = tokensValues[1];

                //通过DI（WebAPI DependencyScope）获取认证服务
                IDependencyScope dependencyScope = request.GetDependencyScope();
                var accountAppService = (AccountAppService) dependencyScope.GetService(typeof (AccountAppService));
                MembershipContext membership = accountAppService.Login(new LoginInputDto
                {
                    Username = userUame,
                    Password = hashPassword
                });

                if (membership.IsAuthenticated)
                {
                    //创建认证Token
                    var identity = new GenericIdentity(membership.UserName);
                    IPrincipal principal = new GenericPrincipal(identity, membership.Roles);
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task; //401 result
                }
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;    //403 result
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
