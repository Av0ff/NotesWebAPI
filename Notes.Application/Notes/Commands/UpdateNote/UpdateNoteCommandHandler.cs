using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.UpdateNote
{
	public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
	{
		private readonly INotesDbContext _dbContext;

		public UpdateNoteCommandHandler(INotesDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Handle(UpdateNoteCommand request, CancellationToken cancellation)
		{
			var entity = await _dbContext.Notes
				.FirstOrDefaultAsync(note => note.Id == request.Id);

			if (entity == null || entity.UserId != request.UserId)
			{
				throw new NotFoundException(nameof(Note), request.Id);
			}

			entity.Title = request.Title;
			entity.Description = request.Description;
			entity.EditDate = DateTime.Now;

			await _dbContext.SaveChangesAsync(cancellation);
		}
	}
}
