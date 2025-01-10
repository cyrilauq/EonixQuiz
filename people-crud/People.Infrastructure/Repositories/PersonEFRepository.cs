using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;
using People.Infrastructure.Data;
using System;

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
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Person?> GetById(Guid personId)
        {
            return await context.People.FindAsync(personId);
        }
    }
}
