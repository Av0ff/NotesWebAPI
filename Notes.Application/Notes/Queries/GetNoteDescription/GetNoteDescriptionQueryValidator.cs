using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Queries.GetNoteDescription
{
	public class GetNoteDescriptionQueryValidator : AbstractValidator<GetNoteDescriptionQuery>
	{
		public GetNoteDescriptionQueryValidator()
		{
			RuleFor(getNoteDescriptionQuery => getNoteDescriptionQuery.Id).NotEqual(Guid.Empty);
			RuleFor(getNoteDescriptionQuery => getNoteDescriptionQuery.UserId).NotEqual(Guid.Empty);
		}
	}
}
