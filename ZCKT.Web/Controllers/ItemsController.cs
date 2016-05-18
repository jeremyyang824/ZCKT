using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZCKT.AppServices;
using ZCKT.DTOs;
using ZCKT.WebApi;

namespace ZCKT.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    [RoutePrefix("api/items")]
    public class ItemsController : ApiControllerBase
    {
        private readonly ItemAppService itemAppService;

        public ItemsController(ItemAppService itemAppService)
        {
            this.itemAppService = itemAppService;
        }

        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <param name="id">物料ID</param>
        /// <returns>物料信息</returns>
        [HttpGet]
        [Route("details/{id}")]
        public HttpResponseMessage Get(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var item = this.itemAppService.GetItem(id);
                return request.CreateResponse(HttpStatusCode.OK, item);
            });
        }

        [HttpGet]
        [Route("roots")]
        public HttpResponseMessage GetRootItems(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var roots = this.itemAppService.GetRootItems();
                return request.CreateResponse(HttpStatusCode.OK, roots);
            });
        }

        [HttpGet]
        [Route("roots/{username}")]
        public HttpResponseMessage GetRootItemsByUser(HttpRequestMessage request, string username)
        {
            return CreateHttpResponse(request, () =>
            {
                var roots = this.itemAppService.GetRootItemsByUser(username);
                return request.CreateResponse(HttpStatusCode.OK, roots);
            });
        }

        [HttpGet]
        [Route("components/{pid}")]
        public HttpResponseMessage GetComponentItems(HttpRequestMessage request, string pid)
        {
            return CreateHttpResponse(request, () =>
            {
                var components = this.itemAppService.GetComponentItems(pid);
                return request.CreateResponse(HttpStatusCode.OK, components);
            });
        }

        [HttpPost]
        [Route("search/{username}")]
        public HttpResponseMessage FindItems(HttpRequestMessage request, ItemSearchInputDto filter, string username)
        {
            return CreateHttpResponse(request, () =>
            {
                var items = this.itemAppService.FindItems(username, filter.SearchKey, filter.SearchValue);
                return request.CreateResponse(HttpStatusCode.OK, items);
            });
        }
    }
}
