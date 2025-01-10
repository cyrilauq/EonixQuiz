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
        public void SetUp()
        {
            // Arrange
            var _contextOptions = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase("PersonEFRepositoryTestsDB")
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
            dbContext.Dispose();
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

        [TestMethod]
        public async Task WhenDeletePersonThatExistThenReturnTrue()
        {
            // Arrange
            Person person = await repository.Add(new Person { Firstname = "Test", Name = "Test" });
            int firstCount = await dbContext.People.CountAsync();

            // Act
            bool deletionResult = await repository.Delete(person.Id);

            // Assert
            Assert.IsTrue(deletionResult);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSuchEntityFoundException))]
        public async Task WhenDeletePersonNotThatExistThenReturnFalse()
        {
            // Arrange
            int firstCount = await dbContext.People.CountAsync();

            // Act
            await repository.Delete(Guid.NewGuid());
        }

        [TestMethod]
        public async Task WhenGetByIdFindEntityThenReturnsIt()
        {
            // Arrange
            Person person = await repository.Add(new Person { Firstname = "Test", Name = "Test" });

            // Act
            Person? foundedEntity = await repository.GetById(person.Id);

            // Assert
            Assert.IsNotNull(foundedEntity);
        }

        [TestMethod]
        public async Task WhenGetByIdNotFindEntityThenReturnsNull()
        {
            // Arrange
            Person person = await repository.Add(new Person { Firstname = "Test", Name = "Test" });

            // Act
            Person? foundedEntity = await repository.GetById(Guid.NewGuid());

            // Assert
            Assert.IsNull(foundedEntity);
        }
    }
}
