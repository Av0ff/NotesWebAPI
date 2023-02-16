using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Queries.GetNoteList
{
	public class NoteListViewModel
	{
		public IList<NoteShortInfoViewModel> Notes { get; set; }
	}
}
