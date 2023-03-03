using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
	{
		private readonly INotesDbContext _dbContext;

		public CreateNoteCommandHandler(INotesDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellation)
		{
			var note = new Note
			{
				UserId = request.UserId,
				Title = request.Title,
				Description = request.Description,
				CreationDate = DateTime.Now,
				EditDate = null,
				Id = Guid.NewGuid(),
			};

			await _dbContext.Notes.AddAsync(note, cancellation);
			await _dbContext.SaveChangesAsync(cancellation);

			return note.Id;
		}
	}
}
