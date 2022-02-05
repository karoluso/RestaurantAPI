using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalPagesCount { get; set; }

        public PageResult(List<T> items, int totalItemsCount, int pageSize,int pageNumber)
        {
            Items = items;
            TotalItems = totalItemsCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPagesCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        }

    }
}
