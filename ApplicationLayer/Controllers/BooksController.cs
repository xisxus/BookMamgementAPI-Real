using DataAccessLayer.Contact;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entiies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBook _bookService;

        public BooksController(IBook bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books/Individual
        [HttpGet("Individual")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse.GeneralResponseData<List<Book>>>> IndividualIndex(string id)
        {
            var response = await _bookService.IndividualIndex(id);
            if (!response.Flag)
                return NotFound(response);

            return Ok(response.Data);
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<ServiceResponse.GeneralResponseData<List<Book>>>> GetBook()
        {
            var response = await _bookService.GetBook();
            if (!response.Flag)
                return NotFound(response);

            return Ok(response.Data);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse.GeneralResponseSingle>> GetBookByID(int id)
        {
            var response = await _bookService.GetBookByID(id);
            if (!response.Flag)
                return NotFound(response);

            return Ok(response.Data);
        }

        // POST: api/Books/Create

        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse.GeneralResponse>> Create([FromForm] BookDTO bookDto, List<IFormFile> photos, string id)
        {
            var response = await _bookService.Create(bookDto, photos, id);
            if (!response.Flag)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        // PUT: api/Books/Update/5
        [Authorize]
        [HttpPut("Update/{bookId}")]
        public async Task<ActionResult<ServiceResponse.GeneralResponse>> Update(int bookId, [FromForm] Book updatedBook, List<IFormFile> newPhotos)
        {
            var response = await _bookService.Update(bookId, updatedBook, newPhotos);
            if (!response.Flag)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        // DELETE: api/Books/Delete/5
        [Authorize]
        [HttpDelete("Delete/{bookId}")]
        public async Task<ActionResult<ServiceResponse.GeneralResponse>> Delete(int bookId)
        {
            var response = await _bookService.Delete(bookId);
            if (!response.Flag)
                return NotFound(response.Message);

            return Ok(response.Message);
        }

        // GET: api/Books/Details/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<ServiceResponse.GeneralResponseSingle>> Details(int id)
        {
            var response = await _bookService.Details(id);
            if (!response.Flag)
                return NotFound(response.Message);

            return Ok(response.Message);
        }

        // POST: api/Books/SubmitReview
        [Authorize]
        [HttpPost("SubmitReview")]
        public async Task<ActionResult<ServiceResponse.GeneralResponse>> SubmitReview(int bookId, int rating, string comment)
        {
            var response = await _bookService.SubmitReview(bookId, rating, comment);
            if (!response.Flag)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }
    }
}
