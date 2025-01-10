using AutoMapper;
using Moq;
using People.Application.DTOs;
using People.Application.Exceptions;
using People.Application.Mappers;
using People.Application.Services;
using People.Application.Validators;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Repositories;

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
    }
}
