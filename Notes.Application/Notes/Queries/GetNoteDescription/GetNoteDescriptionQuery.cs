using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Queries.GetNoteDescription
{
	public class GetNoteDescriptionQuery : IRequest<NoteDescriptionViewModel>
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
	}
}
