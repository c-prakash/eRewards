using System.Collections.Generic;

namespace eRewards.Services.Accounts.API.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public long Count { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<TEntity> Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="data"></param>
        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
