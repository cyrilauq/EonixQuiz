using People.Domain.Entities;

namespace People.Domain.Repositories
{
    public interface IPersonRepository
    {
        public Task<Person> Add(Person person);
        //public Task<Person> Update(Guid personId, Person person);
        //public Task Delete(Guid personId);
        //public Task <IEnumerable<Person>> GetAll();
    }

    //public record GetAllArgs(string? Name, string? Firstname);
}
