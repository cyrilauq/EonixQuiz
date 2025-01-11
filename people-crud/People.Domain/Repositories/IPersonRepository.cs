using People.Domain.Entities;
using System.Linq.Expressions;

namespace People.Domain.Repositories
{
    public interface IPersonRepository
    {
        public Task<Person> Add(Person person);
        public Task<Person> Update(Guid personId, Person person);
        public Task<bool> Delete(Guid personId);
        public Task<IEnumerable<Person>> GetAll(Expression<Func<Person, bool>>? filter = null, PaginatedArgs? paginatedArgs = null);
        public Task<Person?> GetById(Guid personId);
    }

    public record PaginatedArgs(int PageSize, int PageIndex);
}
