using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Tests.Common
{
	public class NotesContextFactory
	{
		public static Guid UserAId = Guid.NewGuid();
		public static Guid UserBId = Guid.NewGuid();

		public static Guid NoteIdDelete = Guid.NewGuid();
		public static Guid NoteIdUpdate = Guid.NewGuid();

		public static NotesDbContext Create()
		{
			var options = new DbContextOptionsBuilder<NotesDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			var context = new NotesDbContext(options);
			context.Database.EnsureCreated();
			context.Notes.AddRange(
				new Note
				{
					CreationDate = DateTime.Today,
					Title = "Title1",
					Description = "Description1",
					EditDate = null,
					Id = Guid.Parse("41A97D48-8B20-4C14-BA18-F8CF8B5DD873"),
					UserId = UserAId
				},
				new Note
				{
					CreationDate = DateTime.Today,
					Title = "Title2",
					Description = "Description2",
					EditDate = null,
					Id = Guid.Parse("572648EC-25A2-4DA4-A086-9EBE927C2D06"),
					UserId = UserBId
				},
				new Note
				{
					CreationDate = DateTime.Today,
					Title = "Title3",
					Description = "Description3",
					EditDate = null,
					Id = NoteIdDelete,
					UserId = UserAId
				},
				new Note
				{
					CreationDate = DateTime.Today,
					Title = "Title4",
					Description = "Description4",
					EditDate = null,
					Id = NoteIdUpdate,
					UserId = UserBId
				}
				);
			context.SaveChanges();
			return context;
		}

		public static void Destroy(NotesDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Dispose();
		}

	}
}
