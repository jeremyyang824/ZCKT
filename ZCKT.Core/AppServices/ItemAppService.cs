using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCKT.DTOs;
using ZCKT.Entities;
using ZCKT.Infrastructure;
using ZCKT.Repositories;

namespace ZCKT.AppServices
{
    public class ItemAppService : BaseAppService
    {
        private readonly PartItemRepository partItemRepository;
        private readonly MemberRepository memberRepository;

        public ItemAppService(
            PartItemRepository partItemRepository,
            MemberRepository memberRepository)
        {
            this.partItemRepository = partItemRepository;
            this.memberRepository = memberRepository;
        }

        /// <summary>
        /// 取得物料信息
        /// </summary>
        public PartItemDto GetItem(string id)
        {
            return this.partItemRepository.GetByKey(id).MapTo<PartItemDto>();
        }

        /// <summary>
        /// 取得根物料（产品）
        /// </summary>
        public IEnumerable<PartItemDto> GetRootItems()
        {
            return this.partItemRepository.GetRootItems(null).MapTo<IEnumerable<PartItemDto>>();
        }

        /// <summary>
        /// 取得用户负责产品
        /// </summary>
        /// <param name="username">用户名</param>
        public IEnumerable<PartItemDto> GetRootItemsByUser(string username)
        {
            var products = this.memberRepository.GetUserProductTypes(username);
            if (!products.Any())
                return new PartItemDto[0];  //none!
            return this.partItemRepository.GetRootItems(products).MapTo<IEnumerable<PartItemDto>>();
        }

        /// <summary>
        /// 取得物料子项
        /// </summary>
        /// <param name="parentId">父项ID</param>
        public IEnumerable<PartItemDto> GetComponentItems(string parentId)
        {
            return this.partItemRepository.GetComponentItems(parentId).MapTo<IEnumerable<PartItemDto>>();
        }
    }
}
