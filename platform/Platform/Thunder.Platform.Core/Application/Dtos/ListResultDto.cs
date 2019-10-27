using System;
using System.Collections.Generic;

namespace Thunder.Platform.Core.Application.Dtos
{
    /// <summary>
    /// Implements <see cref="IListResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list.</typeparam>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        private IReadOnlyList<T> _items;

        public ListResultDto()
        {
        }

        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }

        /// <summary>
        /// List of items.
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
    }
}
