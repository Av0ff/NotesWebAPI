using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
	public class UpdateNoteCommandHandlerTest : TestCommandBase
	{
		[Fact]
		public async Task UpdateNoteCommandHandler_Success()
		{
			//Arrange
			var handler = new UpdateNoteCommandHandler(Context);
			var newTitle = "newTitle";

			//Act
			await handler.Handle(new UpdateNoteCommand
			{
				Id = NotesContextFactory.NoteIdUpdate,
				UserId = NotesContextFactory.UserBId,
				Title = newTitle
			},
			CancellationToken.None);

			//Assert
			Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
				note.Id == NotesContextFactory.NoteIdUpdate &&
				note.Title == newTitle));
		}

		[Fact]
		public async Task UpdateNoteCommandHandler_FailOnWrongId()
		{
			//Arrange
			var handler = new UpdateNoteCommandHandler(Context);

			//Act
			//Assert
			await Assert.ThrowsAsync<NotFoundException>(async () =>
			{
				await handler.Handle(new UpdateNoteCommand
				{
					Id = Guid.NewGuid(),
					UserId = NotesContextFactory.UserBId //UserAId
				},
				CancellationToken.None);
			});
		}

		[Fact]
		public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
		{
			//Arrange
			var handler = new UpdateNoteCommandHandler(Context);

			//Act
			//Assert
			await Assert.ThrowsAsync<NotFoundException>(async () =>
			{
				await handler.Handle(new UpdateNoteCommand
				{
					Id = NotesContextFactory.NoteIdUpdate,
					UserId = NotesContextFactory.UserAId
				},
				CancellationToken.None);
			});
		}
	}
}
