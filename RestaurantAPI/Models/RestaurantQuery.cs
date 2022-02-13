namespace RestaurantAPI.Models
{
    public class RestaurantQuery
    {
        public string SearchPhrase { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string SortBy { get; set; }

        public SortOrderEnum SortOrder { get; set; }

    }
}
