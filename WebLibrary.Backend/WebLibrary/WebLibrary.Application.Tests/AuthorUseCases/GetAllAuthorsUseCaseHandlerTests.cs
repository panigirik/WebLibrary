using AutoMapper;
using Moq;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using Xunit;

namespace WebLibrary.Domain.Tests.AuthorUseCases;

    public class GetAllAuthorsUseCaseHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllAuthorsUseCase _getAllAuthorsUseCase;

        public GetAllAuthorsUseCaseHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _mapperMock = new Mock<IMapper>();
            _getAllAuthorsUseCase = new GetAllAuthorsUseCase(_authorRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnMappedAuthors_WhenAuthorsExist()
        {
            var authors = new List<Author>
            {
                new Author { AuthorId = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 5, 1) },
                new Author { AuthorId = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1975, 3, 12) }
            };

            var authorDtos = authors.Select(a => new AuthorDto
            {
                AuthorId = a.AuthorId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                DateOfBirth = a.DateOfBirth
            }).ToList();
            
            _authorRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(authors);
            
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<AuthorDto>>(authors)).Returns(authorDtos);
            
            var result = await _getAllAuthorsUseCase.ExecuteAsync();
            
            Assert.NotNull(result);
            Assert.Equal(authors.Count, result.Count());
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AuthorDto>>(authors), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoAuthorsExist()
        {
            var authors = new List<Author>();
            
            _authorRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(authors);
            
            var result = await _getAllAuthorsUseCase.ExecuteAsync();
            
            Assert.NotNull(result);
            Assert.Empty(result);  
            _mapperMock.Verify(mapper => mapper.Map<IEnumerable<AuthorDto>>(authors), Times.Once);
        }
    }

