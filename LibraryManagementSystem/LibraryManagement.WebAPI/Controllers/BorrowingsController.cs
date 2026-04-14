using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Commands;
using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Claims;

namespace LibraryManagementSystem.LibraryManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BorrowingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BorrowingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookCommand command)
        {
            // Basic validation without FluentValidation dependency
            if (command.MemberId <= 0 || command.BookId <= 0)
            {
                return BadRequest("MemberId and BookId must be greater than 0");
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

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookCommand command)
        {
            if (command.BorrowingRecordId <= 0)
            {
                return BadRequest("BorrowingRecordId must be greater than 0");
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

        [HttpGet("my-borrowings")]
        public async Task<IActionResult> GetMyBorrowings()
        {
            try
            {
                var memberIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (memberIdClaim == null || !int.TryParse(memberIdClaim.Value, out int memberId))
                {
                    return Unauthorized("User ID not found in token");
                }

                var query = new GetBorrowingsByMemberQuery(memberId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("member/{memberId}")]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> GetBorrowingsByMember(int memberId)
        {
            if (memberId <= 0)
            {
                return BadRequest("MemberId must be greater than 0");
            }

            var query = new GetBorrowingsByMemberQuery(memberId);
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

        [HttpGet("overdue")]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> GetOverdueBorrowings()
        {
            var query = new GetOverdueBorrowingsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
