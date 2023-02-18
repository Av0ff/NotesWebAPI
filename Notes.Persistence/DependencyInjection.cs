using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			var connection = configuration.GetConnectionString("MSSQL");
			services.AddDbContext<NotesDbContext>(opt => opt.UseSqlServer(connection));
			services.AddScoped<INotesDbContext, NotesDbContext>();
			return services;
		}
	}
}
