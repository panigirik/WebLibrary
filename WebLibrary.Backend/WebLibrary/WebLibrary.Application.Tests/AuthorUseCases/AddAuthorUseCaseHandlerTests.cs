using AutoMapper;
using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorUseCases;

    public class AddAuthorUseCaseHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AddAuthorUseCase _addAuthorUseCase;

        public AddAuthorUseCaseHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();
            _addAuthorUseCase = new AddAuthorUseCase(_authorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddAuthor_WhenAuthorDtoIsValid()
        {
            var authorDto = new AuthorDto
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1980, 5, 1)
            };
            var authorEntity = new Author
            {
                AuthorId = Guid.NewGuid(),
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                DateOfBirth = authorDto.DateOfBirth
            };
            
            _mapperMock.Setup(mapper => mapper.Map<Author>(authorDto)).Returns(authorEntity);
            
            await _addAuthorUseCase.ExecuteAsync(authorDto);
            
            _authorRepositoryMock.Verify(repo => repo.AddAsync(authorEntity), Times.Once);
        }

    }

