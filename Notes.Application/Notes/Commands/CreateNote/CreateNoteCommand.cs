using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommand : IRequest<Guid>
	{
		public Guid UserId { get; set; }
		public string Title { get; set; }
		public string Descriprion { get; set; }
	}
}
