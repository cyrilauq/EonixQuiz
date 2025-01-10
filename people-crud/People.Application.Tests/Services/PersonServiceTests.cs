using AutoMapper;
using Moq;
using People.Application.DTOs;
using People.Application.DTOs.Args;
using People.Application.Exceptions;
using People.Application.Mappers;
using People.Application.Services;
using People.Application.Validators;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;
using System.Linq.Expressions;

namespace People.Application.Tests.Services
{
    [TestClass]
    public class PersonServiceTests
    {
        PersonService service;
        Mock<IPersonRepository> mockedPersonRepo;

        [TestInitialize]
        public void SetUp()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PersonMapperProfile());
                mc.AddProfile(new ParginatedArgsMapperProfile());
            });

            mockedPersonRepo = new Mock<IPersonRepository>();

            service = new PersonService(mockedPersonRepo.Object, mappingConfig.CreateMapper(), new PersonValidator());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task WhenRepoThrowExceptionThenCatchItAndThrowItsOwn()
        {
            // Arrange
            mockedPersonRepo.Setup(mpr => mpr.Add(It.IsAny<Person>()))
                .ThrowsAsync(new EntityNotValidException(""));

            // Act
            await service.Add(new PersonDTO());
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public async Task WhenValidationIsNotASuccessThenThrowsValidationException()
        {
            // Act
            await service.Add(new PersonDTO());
        }

        [TestMethod]
        public async Task WhenDeletePersonDeletionIsASuccessThenReturnsTrue()
        {
            // Arrange
            mockedPersonRepo.Setup(mpr => mpr.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // Act
            bool deletionResult = await service.Delete(Guid.NewGuid());

            // Assert
            Assert.IsTrue(deletionResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFound))]
        public async Task WhenRepositoryThrowNotSuchEntityFoundExceptionThenCatchItAndThrowsResourceNotFound()
        {
            // Arrange
            mockedPersonRepo.Setup(mpr => mpr.Delete(It.IsAny<Guid>()))
                .ThrowsAsync(new NotSuchEntityFoundException(""));

            // Act
            bool deletionResult = await service.Delete(Guid.NewGuid());
        }

        [TestMethod]
        public async Task When_GetAll_HasFilteringArgs_ThenReposiIsCalledWithAFilteringFunction()
        {
            // Act
            await service.GetAll(null, new FilteringPersonDTO("nam", null));

            // Assert
            mockedPersonRepo.Verify(r => r.GetAll(It.IsAny<Expression<Func<Person, bool>>>(), null), Times.Once());
        }

        [TestMethod]
        public async Task When_GetAll_HasPaginationArgs_ThenReposiIsCalledWithPaginationArgs()
        {
            // Act
            await service.GetAll(new PaginatedArgsDTO(1, 10), null);

            // Assert
            mockedPersonRepo.Verify(r => r.GetAll(null, new PaginatedArgs(1, 10)), Times.Once());
        }

        [TestMethod]
        public async Task When_GetAll_HasFilteringAndPaginationArgs_ThenReturnsPaginationInformation()
        {
            // Arrange
            mockedPersonRepo.Setup(r => r.GetAll(It.IsAny<Expression<Func<Person, bool>>>(), It.IsAny<PaginatedArgs>()))
                .ReturnsAsync([]);

            // Act
            var result = await service.GetAll(new PaginatedArgsDTO(1, 10), null);

            // Assert
            Assert.IsNotNull(result.PageIndex);
            Assert.IsNotNull(result.PageSize);
        }
    }
}
