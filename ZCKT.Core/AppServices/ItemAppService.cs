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
        public IEnumerable<PartItemWithChildCountDto> GetRootItems()
        {
            return this.partItemRepository.GetRootItems(null).MapTo<IEnumerable<PartItemWithChildCountDto>>();
        }

        /// <summary>
        /// 取得用户负责产品
        /// </summary>
        /// <param name="username">用户名</param>
        public IEnumerable<PartItemWithChildCountDto> GetRootItemsByUser(string username)
        {
            var products = this.memberRepository.GetUserProductTypes(username);
            if (!products.Any())
                return new PartItemWithChildCountDto[0];  //none!
            return this.partItemRepository.GetRootItems(products).MapTo<IEnumerable<PartItemWithChildCountDto>>();
        }

        /// <summary>
        /// 取得物料子项
        /// </summary>
        /// <param name="parentId">父项ID</param>
        public IEnumerable<PartItemWithChildCountDto> GetComponentItems(string parentId)
        {
            return this.partItemRepository.GetComponentItems(parentId).MapTo<IEnumerable<PartItemWithChildCountDto>>();
        }

        /// <summary>
        /// 查找物料
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="searchKey">Content/HomCode/PartName</param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IEnumerable<PartItemWithHintDto> FindItems(string username, string searchKey, string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
                throw new ArgumentNullException("searchKey");
            if (string.IsNullOrWhiteSpace(searchValue))
                throw new ArgumentNullException("searchValue");

            var products = this.memberRepository.GetUserProductTypes(username);
            if (!products.Any())
                return new PartItemWithHintDto[0];  //none!

            IEnumerable<PartItemWithHintDto> results = null;
            if (searchKey == "Content")
            {
                results = this.partItemRepository.FindItemsByContent(products, searchValue)
                    .MapTo<IEnumerable<PartItemWithHintDto>>();
            }
            else if (searchKey == "HomCode")
            {
                results = this.partItemRepository.FindItemsByHomcode(products, searchValue)
                    .MapTo<IEnumerable<PartItemWithHintDto>>();
            }
            else if (searchKey == "PartName")
            {
                results = this.partItemRepository.FindItemsByPartname(products, searchValue)
                    .MapTo<IEnumerable<PartItemWithHintDto>>();
            }
            else
                throw new DomainException("Unknow search key [{0}]!", searchKey);

            return results;
        }
    }
}
