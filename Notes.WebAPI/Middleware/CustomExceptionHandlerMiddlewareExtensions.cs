﻿using Microsoft.AspNetCore.Builder;

namespace Notes.WebAPI.Middleware
{
	public static class CustomExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseMiddleware<CustomExceptionHandlerMiddleware>();
		}
	}
}
