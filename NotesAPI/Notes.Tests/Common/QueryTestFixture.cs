using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Notes.Tests.Common
{
	public class QueryTestFixture : IDisposable
	{
		public NotesDbContext Context;
		public IMapper Mapper;

		public QueryTestFixture()
		{
			Context = NotesContextFactory.Create();
			var configurationProvider = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
			});
			Mapper = configurationProvider.CreateMapper();
		}

		public void Dispose()
		{
			Context.Dispose();
		}
	}

	[CollectionDefinition("QueryCollection")]
	public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
