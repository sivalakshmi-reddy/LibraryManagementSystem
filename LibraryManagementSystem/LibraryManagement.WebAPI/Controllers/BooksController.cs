using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands;
using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Queries;
using LibraryManagementSystem.LibraryManagementSystem.Application.Validators.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace LibraryManagementSystem.LibraryManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBooksQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetBookByIdQuery(id);
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
        {
            var validator = new CreateBookCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBookCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
