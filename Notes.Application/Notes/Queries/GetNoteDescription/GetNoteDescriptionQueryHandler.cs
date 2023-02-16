using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteDescription
{
	public class GetNoteDescriptionQueryHandler : IRequestHandler<GetNoteDescriptionQuery, NoteDescriptionViewModel>
	{
		private readonly INotesDbContext _dbContext;
		private readonly IMapper _mapper;
		public GetNoteDescriptionQueryHandler(INotesDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<NoteDescriptionViewModel> Handle(GetNoteDescriptionQuery request, CancellationToken cancellation)
		{
			var entity = await _dbContext.Notes
				.FirstOrDefaultAsync(note => note.Id == request.Id);

			if (entity == null || entity.UserId != request.UserId)
			{
				throw new NotFoundException(nameof(Note), request.Id);
			}

			return _mapper.Map<Note, NoteDescriptionViewModel>(entity);

		}
	}
}
