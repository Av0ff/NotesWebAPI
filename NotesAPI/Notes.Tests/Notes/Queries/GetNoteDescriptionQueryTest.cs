using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDescription;
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
	public class GetNoteDescriptionQueryTest : TestCommandBase
	{
		private readonly NotesDbContext Context;
		private readonly IMapper Mapper;

		public GetNoteDescriptionQueryTest(QueryTestFixture fixture)
		{
			Context = fixture.Context;
			Mapper = fixture.Mapper;
		}

		[Fact]
		public async Task GetNoteDescriptionQuery_Success()
		{
			//Arrange
			var handler = new GetNoteDescriptionQueryHandler(Context, Mapper);

			//Act
			var result = await handler.Handle(new GetNoteDescriptionQuery
			{
				Id = Guid.Parse("572648EC-25A2-4DA4-A086-9EBE927C2D06"),
				UserId = NotesContextFactory.UserBId
			},
			CancellationToken.None);

			//Assert
			result.ShouldBeOfType<NoteDescriptionViewModel>();
			result.Title.ShouldBe("Title2");
			result.CreationDate.ShouldBe(DateTime.Today);
		}
	}
}
