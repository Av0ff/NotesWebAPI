using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.UpdateNote;
using System;

namespace Notes.WebAPI.Models
{
	public class UpdateNoteViewModel : IMapWith<UpdateNoteCommand>
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		
		public void Mapping(Profile profile)
		{
			profile.CreateMap<UpdateNoteViewModel, UpdateNoteCommand>();
		}
	}
}
