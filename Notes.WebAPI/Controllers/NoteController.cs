﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
	[Route("api/[controller]")]
	public class NoteController : BaseController
	{
		public readonly IMapper _mapper;
		public NoteController(IMapper mapper)
		{
			_mapper = mapper;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<NoteListViewModel>> GetAll()
		{
			var query = new GetNoteListQuery
			{
				UserId = UserId
			};

			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		[HttpGet("{id}")]
		[Authorize]
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

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteViewModel createNote)
		{
			var command = _mapper.Map<CreateNoteCommand>(createNote);
			command.UserId = UserId;
			var noteId = await Mediator.Send(command);
			return Ok(noteId);
		}

		[HttpPut]
		[Authorize]
		public async Task<IActionResult> Update([FromBody] UpdateNoteViewModel updateNote)
		{
			var command = _mapper.Map<UpdateNoteCommand>(updateNote);
			command.UserId = UserId;
			await Mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[Authorize]
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
