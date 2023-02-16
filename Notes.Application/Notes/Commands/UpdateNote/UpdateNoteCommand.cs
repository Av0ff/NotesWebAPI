using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Commands.UpdateNote
{
	public class UpdateNoteCommand : IRequest
	{
		public Guid UserId { get; set; }
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
