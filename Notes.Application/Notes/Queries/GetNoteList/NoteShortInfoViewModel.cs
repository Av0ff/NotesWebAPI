using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Queries.GetNoteList
{
	public class NoteShortInfoViewModel : IMapWith<Note>
	{
		public Guid Id { get; set; }
		public string Title { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Note, NoteShortInfoViewModel>();
		}
	}
}
