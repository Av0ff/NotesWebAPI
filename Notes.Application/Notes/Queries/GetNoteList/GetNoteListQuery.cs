﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Queries.GetNoteList
{
	public class GetNoteListQuery : IRequest<NoteListViewModel>
	{
		public Guid UserId { get; set; }
	}
}
