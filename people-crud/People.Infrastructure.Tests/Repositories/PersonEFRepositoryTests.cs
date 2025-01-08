using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Infrastructure.Data;
using People.Infrastructure.Repositories;

namespace People.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class PersonEFRepositoryTests
    {
        PersonEFRepository repository;
        PeopleDbContext dbContext;

        [TestInitialize]
        public async Task SetUp()
        {
            // Arrange
            var _contextOptions = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase("BloggingControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            dbContext = new PeopleDbContext(_contextOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repository = new PersonEFRepository(dbContext);
        }

        [TestCleanup]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotValidException))]
        public async Task WhenEntityThatDoesntMatchDatabaseSchemaIsGivenThenThrowsDbUpdateException()
        {
            // Act
            await repository.Add(new Person());
        }

        [TestMethod]
        public async Task WhenEntityIsAddedThenPeopleTableHasOneMoreEntry()
        {
            // Arrange
            int firstCount = await dbContext.People.CountAsync();

            // Act
            Person result = await repository.Add(new Person { Firstname = "Test", Name = "Test" });

            // Assert
            Assert.AreEqual(firstCount + 1, await dbContext.People.CountAsync());
        }
    }
}
