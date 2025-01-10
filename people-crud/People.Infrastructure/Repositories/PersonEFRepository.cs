using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;
using People.Infrastructure.Data;
using System.Linq.Expressions;

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

        public async Task<bool> Delete(Guid personId)
        {
            Person? foundedPerson = await GetById(personId);
            if (foundedPerson == null)
            {
                throw new NotSuchEntityFoundException($"No entity Person found with the id [{personId}]");
            }
            context.People.Remove(foundedPerson);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Person>> GetAll(Expression<Func<Person, bool>>? filter = null, PaginatedArgs ? paginatedArgs = null)
        {
            IQueryable<Person> query = context.Set<Person>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(paginatedArgs != null)
            {
                query = query
                    .Skip((paginatedArgs.PageIndex - 1) * paginatedArgs.PageSize)
                    .Take(paginatedArgs.PageSize);
            }
            return await query.ToListAsync();
        }

        public async Task<Person?> GetById(Guid personId)
        {
            return await context.People.FindAsync(personId);
        }
    }
}
