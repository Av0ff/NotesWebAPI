﻿using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Persistence;
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
	public class DeleteNoteCommandHandlerTest : TestCommandBase
	{
		[Fact]
		public async Task DeleteNoteCommandHandler_Success()
		{
			//Arrange
			var handler = new DeleteNoteCommandHandler(Context);

			//Act
			await handler.Handle(new DeleteNoteCommand
			{
				Id = NotesContextFactory.NoteIdDelete,
				UserId = NotesContextFactory.UserAId
			},
			CancellationToken.None);

			//Assert
			Assert.Null(Context.Notes.SingleOrDefault(note =>
				note.Id == NotesContextFactory.NoteIdDelete));
		}

		[Fact]
		public async Task DeleteNoteCommandHandler_FailOnWrongId()
		{
			//Arrange
			var handler = new DeleteNoteCommandHandler(Context);

			//Act
			//Assert
			await Assert.ThrowsAsync<NotFoundException>(async () =>
				await handler.Handle(
					new DeleteNoteCommand
					{
						Id = Guid.NewGuid(),
						UserId = NotesContextFactory.UserAId
					},
					CancellationToken.None));
		}

		[Fact]
		public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
		{
			//Arrange
			var deleteHandler = new DeleteNoteCommandHandler(Context);
			var createHandler = new CreateNoteCommandHandler(Context);
			var noteId = await createHandler.Handle(new CreateNoteCommand {
				Title = "NoteTitle",
				Description = "NoteDescription",
				UserId = NotesContextFactory.UserAId
			},
			CancellationToken.None);

			//Act
			//Assert
			await Assert.ThrowsAsync<NotFoundException>(async () =>
			{
				await deleteHandler.Handle(new DeleteNoteCommand
				{
					Id = noteId,
					UserId = NotesContextFactory.UserBId
				},
				CancellationToken.None);
			});
		}
	}
}
