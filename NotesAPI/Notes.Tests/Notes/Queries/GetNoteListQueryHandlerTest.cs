using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Queries
{
	[Collection("QueryCollection")]
	public class GetNoteListQueryHandlerTest : TestCommandBase
	{
		private readonly NotesDbContext Context;
		private readonly IMapper Mapper;

		public GetNoteListQueryHandlerTest(QueryTestFixture fixture)
		{
			Context = fixture.Context;
			Mapper = fixture.Mapper;
		}

		[Fact]
		public async Task GetNoteListQueryHandler_Success()
		{
			//Arrange
			var handler = new GetNoteListQueryHandler(Context, Mapper);

			//Act
			var result = await handler.Handle(new GetNoteListQuery
			{
				UserId = NotesContextFactory.UserBId
			},
			CancellationToken.None);

			//Assert
			result.ShouldBeOfType<NoteListViewModel>();
			result.Notes.Count.ShouldBe(2);
		}
	}
}
