using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
	public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListViewModel>
	{
		private readonly INotesDbContext _dbContext;
		private readonly IMapper _mapper;
		public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<NoteListViewModel> Handle(GetNoteListQuery request, CancellationToken cancellation)
		{
			var notesQuery = await _dbContext.Notes
				.Where(note => note.UserId == request.UserId)
				.ProjectTo<NoteShortInfoViewModel>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellation);

			return new NoteListViewModel { Notes = notesQuery };
		}
	}
}
