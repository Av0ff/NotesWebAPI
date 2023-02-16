using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Commands.DeleteNote
{
	public class DeleteNoteCommand : IRequest
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
	}
}
