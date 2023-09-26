namespace UnitTestsCourse.Web.Util
{
    public class PaginatedList<T> : List<T>
    {
        private PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            AddRange(items);
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IEnumerable<T> source, HttpRequest request)
        {
            const int pageSize = 10;

            var pageIndex = 
                int.TryParse(request.Query["page"], out var queryPageIndex)
                    ? queryPageIndex
                    : 1;

            var data = source as List<T> ?? source.ToList();
            var count = data.Count;
            var items = Paginate(data, pageIndex, pageSize);

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        private static List<T> Paginate(List<T> data, int pageIndex, int pageSize) => 
            data.Skip(Offset(pageIndex, pageSize))
                .Take(pageSize)
                .ToList();

        private static int Offset(int pageIndex, int pageSize) => 
            (pageIndex - 1) * pageSize;
    }
}
