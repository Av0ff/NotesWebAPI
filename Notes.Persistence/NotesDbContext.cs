using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Persistence.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Persistence
{
	public class NotesDbContext : DbContext, INotesDbContext
	{
		public NotesDbContext(DbContextOptions<NotesDbContext> options)
			:base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Note> Notes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new NoteConfiguration());
		}
	}
}
