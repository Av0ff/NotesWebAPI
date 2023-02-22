using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes.Application.Common.Behaviour
{
	public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation)
		{
			var context = new ValidationContext<TRequest>(request);
			var failures = _validators
				.Select(v => v.Validate(context))
				.SelectMany(v => v.Errors)
				.Where(failure => failure != null)
				.ToList();

			if (failures.Count != 0)
			{
				throw new ValidationException(failures);
			}

			return next();
		}
	}
}
