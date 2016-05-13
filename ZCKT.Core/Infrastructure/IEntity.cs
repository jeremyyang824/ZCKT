namespace ZCKT.Infrastructure
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
