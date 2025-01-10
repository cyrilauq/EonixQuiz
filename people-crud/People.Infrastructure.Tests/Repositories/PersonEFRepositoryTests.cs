using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;
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

            // Act
            bool deletionResult = await repository.Delete(person.Id);

            // Assert
            Assert.IsTrue(deletionResult);
            Assert.AreEqual(0, await dbContext.People.CountAsync());
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

        [TestMethod]
        public async Task When_GetAll_WithNoFilteringAndNoPagination_ThenReturnsAllPeople()
        {
            // Arrange
            await repository.Add(new Person { Firstname = "Test", Name = "Test" });
            await repository.Add(new Person { Firstname = "Test", Name = "Test" });
            await repository.Add(new Person { Firstname = "Test", Name = "Test" });
            await repository.Add(new Person { Firstname = "Test", Name = "Test" });

            // Act
            IEnumerable<Person> allPeople = await repository.GetAll();

            // Assert
            Assert.AreEqual(4, allPeople.Count());
        }

        [TestMethod]
        public async Task When_GetAll_WithFilteringAndNoPagination_ThenReturnsAllPeopleThatCorrespondToTheFilter()
        {
            // Arrange
            await repository.Add(new Person { Firstname = "Cyril", Name = "Test" });
            await repository.Add(new Person { Firstname = "Cyrielle", Name = "Test" });
            await repository.Add(new Person { Firstname = "marcyr", Name = "Test" });
            await repository.Add(new Person { Firstname = "Test", Name = "Test" });

            // Act
            IEnumerable<Person> allPeople = await repository.GetAll(person => person.Firstname.ToLower().Contains("cyr"));

            // Assert
            Assert.AreEqual(3, allPeople.Count());
        }

        [TestMethod]
        public async Task When_GetAll_WithPaginationAndPageSizeGreaterThanDbCollectionCount_ThenReturnAllDbCollectionResult()
        {
            // Arrange
            await repository.Add(new Person { Firstname = "Cyril", Name = "Test" });
            await repository.Add(new Person { Firstname = "Cyrielle", Name = "Test" });
            await repository.Add(new Person { Firstname = "marcyr", Name = "Test" });
            await repository.Add(new Person { Firstname = "Test", Name = "Test" });

            // Act
            IEnumerable<Person> allPeople = await repository.GetAll(paginatedArgs: new PaginatedArgs(10, 1));

            // Assert
            Assert.AreEqual(4, allPeople.Count());
        }

        [TestMethod]
        public async Task When_GetAll_WithPaginationAndPageSizeSmallerThanDbCollectionCount_ThenReturnCollectionOfSizePageSize()
        {
            // Arrange
            await repository.Add(new Person { Firstname = "Cyril", Name = "Test" });
            await repository.Add(new Person { Firstname = "Cyrielle", Name = "Test" });
            await repository.Add(new Person { Firstname = "marcyr", Name = "Test" });
            await repository.Add(new Person { Firstname = "Test", Name = "Test" });

            // Act
            IEnumerable<Person> allPeople = await repository.GetAll(paginatedArgs: new PaginatedArgs(1, 1));

            // Assert
            Assert.AreEqual(1, allPeople.Count());
        }
    }
}
