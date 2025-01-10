using People.Domain.Entities;

namespace People.Domain.Repositories
{
    public interface IPersonRepository
    {
        public Task<Person> Add(Person person);
        //public Task<Person> Update(Guid personId, Person person);
        public Task<bool> Delete(Guid personId);
        //public Task <IEnumerable<Person>> GetAll();
        public Task<Person?> GetById(Guid personId);
    }

    //public record GetAllArgs(string? Name, string? Firstname);
}
