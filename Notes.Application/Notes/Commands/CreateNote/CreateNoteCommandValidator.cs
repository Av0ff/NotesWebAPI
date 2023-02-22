using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
	{
		public CreateNoteCommandValidator()
		{
			RuleFor(createNoteCommand => createNoteCommand.UserId).NotEqual(Guid.Empty);
			RuleFor(createNoteCommand => createNoteCommand.Title).NotEmpty().MaximumLength(250);
		}
	}
}
