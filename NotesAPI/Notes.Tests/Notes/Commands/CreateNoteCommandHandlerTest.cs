using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
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
	public class CreateNoteCommandHandlerTest : TestCommandBase
	{
		[Fact]
		public async Task CreateNoteCommandHandler_Success()
		{
			//Arrange
			var handler = new CreateNoteCommandHandler(Context);
			var noteName = "note name";
			var noteDescription = "note description";

			//Act
			var noteId = await handler.Handle(
				new CreateNoteCommand
				{
					Title = noteName,
					Description = noteDescription,
					UserId = NotesContextFactory.UserAId
				},
				CancellationToken.None);

			//Assert
			Assert.NotNull(
				await Context.Notes.SingleOrDefaultAsync(note =>
					note.Id == noteId && note.Title == noteName && note.Description == noteDescription));
		}
	}
}
