namespace RoutineTracker.Server.Common
{
    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords, string baseUrl)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            PreviousPageUrl = pageNumber > 1 ? $"{baseUrl}?pageNumber={pageNumber - 1}&pageSize={pageSize}" : null;
            NextPageUrl = pageNumber < TotalPages ? $"{baseUrl}?pageNumber={pageNumber + 1}&pageSize={pageSize}" : null;
        }

        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public string? PreviousPageUrl { get; set; }
        public string? NextPageUrl { get; set; }

        public static PagedResponse<T> Instance(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords, string baseUrl)
            => new(data, pageNumber, pageSize, totalRecords, baseUrl);
    }

}
