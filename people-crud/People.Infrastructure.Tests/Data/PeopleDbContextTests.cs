using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using People.Infrastructure.Data;

namespace People.Infrastructure.Tests.Data
{
    [TestClass]
    public class PeopleDbContextTests
    {
        PeopleDbContext dbContext;

        [TestInitialize]
        public void SetUp()
        {
            // Arrange
            var _contextOptions = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase("BloggingControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            dbContext = new PeopleDbContext(_contextOptions);

            dbContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [TestMethod]
        public async Task WhenAddANewPeopleEntryThenTablePeopleHasCountTo1()
        {
            Assert.AreEqual(0, await dbContext.People.CountAsync());

            dbContext.People.Add(new Domain.Entities.Person { Name = "Coucou", Firstname = "Coucou" });
            await dbContext.SaveChangesAsync();

            Assert.AreEqual(1, await dbContext.People.CountAsync());
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task WhenAddingPeopleWithoutNameThenThrowDbUpdateException()
        {
            // Act
            dbContext.People.Add(new Domain.Entities.Person { Firstname = "Test" });
            await dbContext.SaveChangesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task WhenAddingPeopleWithoutFirstnameThenThrowDbUpdateException()
        {
            // Act
            dbContext.People.Add(new Domain.Entities.Person { Name = "Test " });
            await dbContext.SaveChangesAsync();
        }

        [TestMethod]
        public async Task WhenAddAPersonThenReturnEntityWithGeneratedId()
        {
            // Act
            var entity = await dbContext.People.AddAsync(new Domain.Entities.Person { Name = "Test ", Firstname = "Test " });
            await dbContext.SaveChangesAsync();

            // Assert
            Assert.AreNotEqual(Guid.Empty, entity.Entity.Id);
        }
    }
}
