using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDescription;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace Notes.WebAPI.Controllers
{
	[ApiVersion("1.0")]
	[Produces("application/json")]
	[Route("api/{version:apiVersion}/[controller]")]
	public class NoteController : BaseController
	{
		public readonly IMapper _mapper;
		public NoteController(IMapper mapper)
		{
			_mapper = mapper;
		}
		/// <summary>
		/// Gets list of all notes
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /note
		/// </remarks>
		/// <returns>Returns NoteListViewModel</returns>
		/// <response code="200">Success</response>
		/// <response code="401">User is unauthorized</response>
		[HttpGet]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<NoteListViewModel>> GetAll()
		{
			var query = new GetNoteListQuery
			{
				UserId = UserId
			};

			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		/// <summary>
		/// Get the note by id
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /note/E6B6C48F-A102-4D6E-B674-EE6932731ACE
		/// </remarks>
		/// <param name="id">Note id(GUID)</param>
		/// <returns>Returns NoteDescriptionViewModel</returns>
		/// <response code="200">Success</response>
		/// <response code="401">User is unauthorized</response>
		[HttpGet("{id}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<NoteDescriptionViewModel>> Get (Guid id)
		{
			var query = new GetNoteDescriptionQuery
			{
				UserId = UserId,
				Id = id
			};

			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		/// <summary>
		/// Creates new note
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// POST /note
		/// {
		///		title: "note title",
		///		details: "note details"
		/// }
		/// </remarks>
		/// <param name="createNote">CreateNoteViewModel object</param>
		/// <returns>Returns id(Guid)</returns>
		/// <response code="201">Success</response>
		/// <response code="401">User is unauthorized</response>
		[HttpPost]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteViewModel createNote)
		{
			var command = _mapper.Map<CreateNoteCommand>(createNote);
			command.UserId = UserId;
			var noteId = await Mediator.Send(command);
			//return Ok(noteId);
			return Created($"https://localhost:5001/api/1.0/Note/{noteId.ToString()}", noteId);
		}

		/// <summary>
		/// Updates note
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// PUT /note
		/// {
		///		title: "Updated title"
		/// }
		/// </remarks>
		/// <param name="updateNote">UpdateNoteViewModel object</param>
		/// <returns>Returns NoContent</returns>
		/// <response code="204">Success</response>		
		/// <response code="401">User is unauthorized</response>
		[HttpPut]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Update([FromBody] UpdateNoteViewModel updateNote)
		{
			var command = _mapper.Map<UpdateNoteCommand>(updateNote);
			command.UserId = UserId;
			await Mediator.Send(command);
			return NoContent();
		}

		/// <summary>
		/// Delete note by id
		/// </summary>
		/// <remarks>
		/// Simple request:
		/// DELETE /note/E6B6C48F-A102-4D6E-B674-EE6932731ACE
		/// </remarks>
		/// <param name="id">Id of the note(Guid)</param>
		/// <returns>Returns NoContent</returns>
		/// <response code="204">Success</response>
		/// <response code="401">User is unauthorized</response>
		[HttpDelete("{id}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Delete(Guid id)
		{
			var command = new DeleteNoteCommand
			{
				Id = id,
				UserId = UserId
			};

			await Mediator.Send(command);
			return NoContent();
		}
	}
}
