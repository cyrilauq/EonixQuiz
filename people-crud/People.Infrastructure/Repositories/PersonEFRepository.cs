using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;
using People.Infrastructure.Data;

namespace People.Infrastructure.Repositories
{
    public class PersonEFRepository(PeopleDbContext context) : IPersonRepository
    {
        public async Task<Person> Add(Person person)
        {
            try
            {
                EntityEntry<Person> entity = await context.People.AddAsync(person);
                await context.SaveChangesAsync();
                return entity.Entity;
            }
            catch(DbUpdateException)
            {
                throw new EntityNotValidException("The given entity lack of information");
            }
        }
    }
}
