namespace People.Application.DTOs.Args
{
    public record PaginatedArgsDTO(int PageSize = int.MaxValue, int PageIndex = 1);
}
