namespace People.Application.DTOs
{
    public class PaginatedListDTOs<T>(IEnumerable<T> list, int? pageSize, int? pageIndex)
    {
        public IEnumerable<T> Result { get => list; }
        public int? PageSize { get => pageSize; }
        public int? PageIndex { get => pageIndex; }
    }
}
