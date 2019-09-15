using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ha_shemNetworksApi.Api.ServiceInterfaces;
using Ha_shemNetworksApiCommon.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ha_shemNetworksApi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAllBooks()
        {
            return Ok(_bookService.GetAll());
        }

        [HttpPost("add")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult Add([FromBody]BookRegistrationDO book)
        {
            if (book.IsValid())
            {
                var result = _bookService.Add(new Book
                {
                    Author = book.Author,
                    Available = true,
                    ISBN = book.ISBN,
                    Status = true,
                    Title = book.Title
                });
                return Ok(result);
            }
            return BadRequest(new { message = "Invalid input(s)" });
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("remove")]
        public IActionResult Remove([FromBody]BookDetails book)
        {
            if (ModelState.IsValid)
            {
                var result = _bookService.Remove(book.bookId);
                return Ok(result);
            }
            return BadRequest(new { message = "Invalid input(s)" });
        }

        [HttpPost("borrow")]
        public IActionResult Borrow([FromBody]BookDetails book)
        {
            if (ModelState.IsValid)
            {
                var result = _bookService.Borrow(book.bookId, book.userId);
                return Ok(result);
            }
            return BadRequest(new { message = "Invalid input(s)" });
        }

        [HttpPost("return")]
        public IActionResult Return([FromBody]BookDetails book)
        {
            if (ModelState.IsValid)
            {
                var result = _bookService.Return(book.bookId, book.userId);
                return Ok(result);
            }
            return BadRequest(new { message = "Invalid input(s)" });
        }

    }
}