using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Queries.GetNoteDescription
{
	public class NoteDescriptionViewModel : IMapWith<Note>
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? EditDate { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Note, NoteDescriptionViewModel>();
		}

	}
}
