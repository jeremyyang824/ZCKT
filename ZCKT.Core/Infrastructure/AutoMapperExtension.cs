using AutoMapper;

namespace ZCKT.Infrastructure
{
    public static class AutoMapperExtension
    {
        public static TResult MapTo<TResult>(this object source)
            where TResult : class
        {
            if (source == null)
                return null;
            return (TResult)Mapper.Map(source, source.GetType(), typeof(TResult));
        }

        //public static PaginationResult<TResult> MapToPagination<TResult>(this IPagedList sourceList)
        //{
        //    if (sourceList == null)
        //        return null;

        //    return new PaginationResult<TResult>
        //    {
        //        CurrentPageIndex = sourceList.CurrentPageIndex,
        //        PageSize = sourceList.PageSize,
        //        TotalItemCount = sourceList.TotalItemCount,
        //        Results = ((IEnumerable<object>)sourceList).Select(source => (TResult)Mapper.Map(source, source.GetType(), typeof(TResult))).ToList()
        //    };
        //}
    }
}
