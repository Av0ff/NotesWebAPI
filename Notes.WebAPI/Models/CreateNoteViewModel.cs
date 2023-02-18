using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.WebAPI.Models
{
	public class CreateNoteViewModel : IMapWith<CreateNoteCommand>
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<CreateNoteViewModel, CreateNoteCommand>();
		}
	}
}
