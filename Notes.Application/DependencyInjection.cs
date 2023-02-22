using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Behaviour;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Notes.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			return services;
		}
	}
}
