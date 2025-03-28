using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Filters;

namespace WebLibrary.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookRequestDto>> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            
            return Ok(book);
        }

        // GET: api/books/isbn/{isbn}
        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<GetBookRequestDto>> GetBookByIsbn(string isbn)
        {
            var book = await _bookService.GetBookByIsbnAsync(isbn);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        
        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetPaginatedBooks([FromQuery] PaginatedBookFilter filter)
        {
            var books = await _bookService.GetPaginatedBooksAsync(filter);
            return Ok(books);
        }


        // GET: api/books/author/{authorId}
        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<GetBookRequestDto>>> GetBooksByAuthor(Guid authorId)
        {
            var books = await _bookService.GetBooksByAuthorAsync(authorId);
            return Ok(books);
        }

        // POST: api/books
        [HttpPost]
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromForm] BookDto bookDto)
        {
            if (bookDto.ImageFile != null)
            {
                using var memoryStream = new MemoryStream();
                await bookDto.ImageFile.CopyToAsync(memoryStream);
            }

            await _bookService.AddBookAsync(bookDto);
            return Ok();
        }


        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetBookImage(Guid id)
        {
            var imageData = await _bookService.GetBookImageAsync(id);
            if (imageData == null)
            {
                return NotFound("Изображение не найдено");
            }

            return File(imageData, "image/jpeg"); // Возвращаем изображение как файл
        }
        
        [HttpPost("{bookId}/borrow")]
        public async Task<IActionResult> BorrowBook(Guid bookId, [FromQuery] Guid userId)
        {
            var success = await _bookService.BorrowBookAsync(bookId, userId);
            if (!success)
                return BadRequest("Book is not available or does not exist.");

            return Ok("Book successfully borrowed.");
        }
        
        [HttpPost("{bookId}/return")]
        public async Task<IActionResult> ReturnBook(Guid bookId, [FromQuery] Guid userId)
        {
            var success = await _bookService.ReturnBookAsync(bookId, userId);
            if (!success)
                return BadRequest("Book cannot be returned or does not belong to this user.");

            return Ok("Book successfully returned.");
        }

        // PUT: api/books
        [HttpPut]
        public async Task<ActionResult> UpdateBook([FromBody] BookDto bookDto)
        {
            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
        
        [HttpDelete("image/{id}")]
        public async Task<IActionResult> RemoveBookImage(Guid id)
        {
            await _bookService.RemoveBookFromCache(id);
            return NoContent();
        }
    }

